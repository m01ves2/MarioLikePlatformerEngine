using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Systems.Collisions;
using MarioLikePlatformerEngine.Systems.Physics;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace MarioLikePlatformerEngine.Scenes
{
    public class GameScene : Scene
    {
        public GameState State { get; private set; } = GameState.Playing;
        private GameContext _context;
        public override GameCommand Command => _context.Command;

        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        private TileMap _map;
        private PlayerEntity _player;

        public GameScene()
        {
            _rules = new CollisionRulesSystem();
            //_rules.AddRule(new PlayerGroundRule());
            _rules.AddRule(new PlayerEnemyStompRule());
            _rules.AddRule(new PlayerEnemyDamageRule());

            _physics = new PhysicsSystem();
        }

        public override void Initialize()
        {
            _map = new TileMap(50, 20);

            // левая стена
            for (int y = 0; y < _map.HeightInTiles; y++)
                _map.SetSolid(0, y);

            // правая стена
            for (int y = 0; y < _map.HeightInTiles; y++)
                _map.SetSolid(_map.WidthInTiles - 1, y);

            // потолок
            for (int x = 0; x < _map.WidthInTiles; x++)
                _map.SetSolid(x, 0);

            // земля
            for (int x = 2; x < 50; x++)
                _map.SetSolid(x, 19);

            // небольшие платформы
            for (int x = 5; x < 10; x++)
                _map.SetSolid(x, 15);

            for (int x = 20; x < 25; x++)
                _map.SetSolid(x, 12);

            _map.SetSolid(20, 18);

            _player = new PlayerEntity(new Vector2(100, 588), 20, 20);
            AddEntity(_player);
            AddEntity(new EnemyEntity(new Vector2(500, 588), 20, 20));


            _context = new GameContext() { Map = _map, State = State };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            DrawTileMap(spriteBatch, _resources.WhitePixel);
            DrawEntities(spriteBatch);

            spriteBatch.End();
        }

        protected void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities)
                entity.Draw(spriteBatch);
        }
        public void DrawTileMap(SpriteBatch sb, Texture2D pixel)
        {
            for (int y = 0; y < 20; y++) {
                for (int x = 0; x < 50; x++) {
                    if (_map.IsSolid(x, y)) {
                        sb.Draw(pixel,
                            new Rectangle(
                                x * _map.TileSize,
                                y * _map.TileSize,
                                _map.TileSize,
                                _map.TileSize
                            ),
                            Color.DarkGreen
                        );
                    }
                }
            }
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

            _rules.Apply(events);

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
                _player.IsDead = true;
            }

            return _player.IsDead;
        }
    }
}
