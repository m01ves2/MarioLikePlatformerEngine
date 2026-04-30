using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.Resources;
using MarioLikePlatformerEngine.Systems.Collisions;
using MarioLikePlatformerEngine.Systems.GameplayRules;
using MarioLikePlatformerEngine.Systems.Physics;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;


namespace MarioLikePlatformerEngine.Scenes
{
    public class GameScene : Scene
    {
        private GameContext _context;

        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        private TileMap _map;
        private PlayerEntity _player;
        private Rectangle _goal;

        private TextureProvider _textures;
        private GameSettings _gameSettings;

        public GameScene(TextureProvider textures, GameSettings gameSettings)
        {
            _rules = new CollisionRulesSystem();
            //_rules.AddRule(new PlayerGroundRule());
            _rules.AddRule(new PlayerEnemyStompRule());
            _rules.AddRule(new PlayerEnemyDamageRule());
            _rules.AddRule(new PlayerCoinCollectRule());

            _physics = new PhysicsSystem();

            _gameSettings = gameSettings;
            _textures = textures;

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

            _context = new GameContext() { Map = _map, 
                State = GameState.Playing, 
                Scores = 0, 
                Command = GameCommand.None,
                Lives = 3};
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            DrawTileMap(spriteBatch, _resources.WhitePixel);
            DrawEntities(spriteBatch);
            DrawGoal(spriteBatch);
            spriteBatch.End();
        }

        protected void DrawEntities(SpriteBatch spriteBatch)
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
                _resources.WhitePixel,
                _goal,
                Color.Gold
            );
        }

        public override void Update(float dt)
        {
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

            _rules.Apply(events, _context);

            _entities.RemoveAll(e => e.IsPendingDestroy);

            var screenCenter = new Vector2(_resources.ScreenWidth / 2f, _resources.ScreenHeight / 2f);
            _camera.Position = _player.Position - screenCenter;
            //Debug.WriteLine("CAMERA UPDATE, " + _camera.Position);

            _camera.Position.X = MathHelper.Clamp(_camera.Position.X, 0, _map.Width - _resources.ScreenWidth);
            _camera.Position.Y = MathHelper.Clamp(_camera.Position.Y, 0, _map.Height - _resources.ScreenHeight);
            //System.Diagnostics.Debug.WriteLine(_player.Position);
            //System.Diagnostics.Debug.WriteLine(_entities.OfType<PlayerEntity>().First().Position);


            if (_context.State == GameState.Dead) {
                RestartLevel();
            }

            if (isGameOver()) {
                _context.Command = GameCommand.Restart;
            }

            if (iskWin()) {
                _context.Command = GameCommand.Restart;
            }
        }

        public void RestartLevel()
        {
            _entities.Clear();
            _map = null;
            _player = null;

            Initialize();
        }

        private bool isGameOver()
        {
            if (_player.Position.Y > _context.Map.Height + 200) {
                _player.Kill();
            }

            if (_player.IsDead)
                _context.Lives--;

            return _context.Lives <= 0;
        }

        private bool iskWin()
        {
            if (_player.Bounds.Intersects(_goal)) {
                _context.State = GameState.Win;
                return true;
            }
            return false;
        }

        public override GameCommand ConsumeCommand()
        {
            var cmd = _context.Command;
            _context.Command = GameCommand.None;
            return cmd;
        }
    }
}
