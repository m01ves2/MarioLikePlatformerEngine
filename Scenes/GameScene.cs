using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Components.Behavior;
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

        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        private TileMap _map;
        private PlayerEntity _player;
        private Rectangle _goal;
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
            LevelData data = LevelBuilder.CreateLevel();
            _map = data.Map;
            _player = data.PlayerStart;
            AddEntity(_player);

            var entities = data.Enemies;
            _goal = data.Goal;

            for (int i = 0; i < entities.Count; i++) {
                AddEntity(entities[i]);
            }

            _context = new GameContext() { Map = _map, State = State };
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

            return _player.IsDead;
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
