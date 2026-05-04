using MarioLikePlatformerEngine.Application.LevelBuilders;
using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Resources;
using MarioLikePlatformerEngine.Systems.Collisions;
using MarioLikePlatformerEngine.Systems.GameplayRules;
using MarioLikePlatformerEngine.Systems.Physics;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace MarioLikePlatformerEngine.Application.Scenes
{
    public class GameScene : Scene
    {
        private GameSession _session;

        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        private TileMap _map;
        private PlayerEntity _player;
        private Rectangle _goal;

        private TextureProvider _textures;
        private SoundsProvider _sounds;

        private GameSettings _gameSettings;

        public GameScene(TextureProvider textures, SoundsProvider sounds, GameSettings gameSettings)
        {
            _rules = new CollisionRulesSystem();
            //_rules.AddRule(new PlayerGroundRule());
            _rules.AddRule(new PlayerEnemyStompRule());
            _rules.AddRule(new PlayerEnemyDamageRule());
            _rules.AddRule(new PlayerCoinCollectRule());

            _physics = new PhysicsSystem();

            _gameSettings = gameSettings;
            _textures = textures;
            _sounds = sounds;

            //StartGame();
        }

        //private void LoadGameResources(GameResources gameResources)
        //{
        //    _font = gameResources.Font;
        //    _screenHeight = gameResources.ScreenHeight;
        //    _screenWidth = gameResources.ScreenWidth;
        //    _pixel = gameResources._whitePixel;
        //}

        //private void LoadGameSettings(GameSettings gameSettings)
        //{
        //    _levelPath = gameSettings.SelectedLevel;
        //}

        public override void Initialize()
        {
            LevelData data = LevelBuilder.CreateLevel();
            _map = data.Map;
            _player = data.PlayerStart;
            AddEntity(_player);

            var entities = data.Enemies;
            _goal = data.Goal;

            for (int i = 0; i < entities.Count; i++) {
                AddEntity(entities[i]);
            }

            var coins = data.Coins;
            for (int i = 0; i < coins.Count; i++) {
                AddEntity(coins[i]);
            }

            _session = new GameSession() {
                Scores = 0,
                Lives = 3 
            };

            StartMusic();
        }

        public void StartMusic()
        {
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_resources.GameMusic);
        }

        public override SceneUpdateResult Update(float dt)
        {
            if(Input.IsKeyPressed(Keys.Escape)) {
                return new SceneUpdateResult(GameCommand.GoToMenu, new GameResult() { Score = _session.Scores });
            }

                foreach (var e in _entities) {
                if (e is EnemyEntity enemy) {
                    enemy.Sense(_map);
                }
            }

            // 1. input / AI
            foreach (var e in _entities)
                e.Update(dt); // ТОЛЬКО input / AI

            // 2. physics + collisions
            List<CollisionEvent> events = new List<CollisionEvent>();
            _physics.Step(_entities, _map, dt, events);
            _rules.Apply(events, _session);

            MakeSounds();
            _entities.RemoveAll(e => e.IsPendingDestroy);

            UpdateCamera();

            //if (_session.State == GameState.GameOver) {
            //    RestartLevel();
            //}

            if (isGameOver()) {
                return new SceneUpdateResult(GameCommand.ShowGameOver, new GameResult() { Score = _session.Scores });
            }

            if (iskWin()) {
                return new SceneUpdateResult(GameCommand.ShowGameWin, new GameResult() { Score = _session.Scores });
            }


            return SceneUpdateResult.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawUI(spriteBatch);
            DrawWorld(spriteBatch);
        }

        private void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(
                _resources.Font,
                $"Score: {_session.Scores}",
                new Vector2(50, 20),
                Color.White
            );

            spriteBatch.End();
        }
        private void DrawWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(_resources.Scale, _resources.ScreenWidth, _resources.ScreenHeight));

            DrawBackground(spriteBatch); // деревья, замок
            DrawTileMap(spriteBatch, _resources.WhitePixel);
            DrawEntities(spriteBatch);
            DrawGoal(spriteBatch);

            spriteBatch.End();
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            DrawBackgroundSky(spriteBatch);
            DrawBackgroundTrees(spriteBatch);
            DrawBackGroundCastle(spriteBatch);
        }
        private void DrawBackgroundSky(SpriteBatch spriteBatch)
        {
            var texture = _textures.Get(BackgroundType.Sky);

            float parallax = 0.25f;

            float screenWidth = _resources.ScreenWidth;
            float screenHeight = _resources.ScreenHeight;

            float y = screenHeight * 0.5f;

            float cameraX = _camera.Position.X * parallax;

            // старт всегда выравниваем по сетке
            //float startX = MathF.Floor(cameraX / texture.Width) * texture.Width - texture.Width;
            var startX = 0;

            //for (float x = startX; x < cameraX + screenWidth + texture.Width; x += texture.Width) {
            for (float x = startX; x < _map.Width; x += texture.Width) {
                spriteBatch.Draw(texture, new Vector2(x - cameraX, y), Color.White);
            }
        }
        private void DrawBackgroundTrees(SpriteBatch spriteBatch)
        {
            var groundLevel = _map.Height;
            var texture = _textures.Get(BackgroundType.Trees);
            var textureWidth = texture.Width;
            var textureHeight = texture.Height;

            int y = groundLevel - textureHeight - _map.TileSize;

            for (int x = 0; x < _map.Width; x += textureWidth) {
                spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
            }
        }
        public void DrawBackGroundCastle(SpriteBatch spriteBatch)
        {
            var castleTexture = _textures.Get(BackgroundType.Castle);
            spriteBatch.Draw(
                castleTexture,
                new Rectangle(
                    _map.Width - castleTexture.Width - _map.TileSize,
                    _map.Height - castleTexture.Height - _map.TileSize,
                    castleTexture.Width, castleTexture.Height),
                Color.White
            );
        }


        private void DrawEntities(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(_font, "Score: 100", new Vector2(10, 10), Color.White);
            foreach (var entity in _entities) {
                var texture = _textures.Get(entity);
                entity.Draw(spriteBatch, texture);
            }
        }
        public void DrawTileMap(SpriteBatch sb, Texture2D pixel)
        {
            for (int y = 0; y < _map.HeightInTiles; y++) {
                for (int x = 0; x < _map.WidthInTiles; x++) {
                    var tile = _map.GetTile(x, y);
                    if (tile == TileType.Empty) continue;

                    var texture = _textures.Get(tile);
                    sb.Draw(texture,
                              new Rectangle(
                                  x * _map.TileSize,
                                  y * _map.TileSize,
                                  _map.TileSize,
                                  _map.TileSize
                              ),
                              Color.White
                          );
                }
            }
        }
        public void DrawGoal(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _textures.Get(BackgroundType.Goal),
                _goal,
                Color.Gold
            );
        }
        public void MakeSounds()
        {
            if(_player.JustJumped)
                _sounds.Get(SoundType.Jump).Play();

            foreach (var e in _entities) {
                if(e is CoinEntity coin && coin.WasCollected) {
                        _sounds.Get(SoundType.Coin).Play(0.2f, 0, 0); //volume: 0.2f, 0, 0
                    coin.WasCollected = false;
                }

                if(e is EnemyEntity enemy && enemy.WasKilled) {
                    _sounds.Get(SoundType.Kickkill).Play();
                    enemy.WasKilled = false;
                }

                if (e is FlyingEnemyEntity flyingenemy && flyingenemy.WasKilled) {
                    _sounds.Get(SoundType.Kickkill).Play();
                    flyingenemy.WasKilled = false;
                }
            }
        }   

        public void UpdateCamera()
        {
            var screenCenter = new Vector2(_resources.ScreenWidth / (2f * _resources.Scale), _resources.ScreenHeight / (2f * _resources.Scale));

            screenCenter.Y *= 0.7f;
            _camera.Position = _player.Position - screenCenter;

            var viewWidth = _resources.ScreenWidth / _resources.Scale;
            var viewHeight = _resources.ScreenHeight / _resources.Scale;

            _camera.Position.X = MathHelper.Clamp(_camera.Position.X, 0, _map.Width - viewWidth);
            _camera.Position.Y = MathHelper.Clamp(_camera.Position.Y, 0, _map.Height - viewHeight);

            //System.Diagnostics.Debug.WriteLine(_player.Position);
            //System.Diagnostics.Debug.WriteLine(_entities.OfType<PlayerEntity>().First().Position);

        }

        private bool isGameOver()
        {
            if (_player.Position.Y > _map.Height + 200) {
                _player.Kill();
            }

            if (_player.IsDead)
                _session.Lives--;

            if(_session.Lives <= 0) {
                return true;
            }
            return false;
        }

        private bool iskWin()
        {
            if (_player.Bounds.Intersects(_goal)) {
                return true;
            }
            return false;
        }
    }
}
