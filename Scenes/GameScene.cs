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
        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        private TileMap _map;
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
            //AddEntity(new PlayerEntity(new Vector2(100, 100), 20, 20));

            //_map = new TileMap(50, 20);
            //for (int x = 0; x < 50; x++)
            //    _map.SetSolid(x, 17);

            ////AddEntity(new EnemyEntity(new Vector2(500, 480), 20, 20));
            ///
            _map = new TileMap(50, 20);

            // земля
            for (int x = 2; x < 50; x++)
                _map.SetSolid(x, 18);

            // небольшие платформы
            for (int x = 5; x < 10; x++)
                _map.SetSolid(x, 15);

            for (int x = 20; x < 25; x++)
                _map.SetSolid(x, 12);

            _map.SetSolid(20, 17);

            AddEntity(new PlayerEntity(new Vector2(100, 100), 20, 20));
            AddEntity(new EnemyEntity(new Vector2(500, 556), 20, 20));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTileMap(spriteBatch, _resources.WhitePixel);
            base.Draw(spriteBatch);
        }

        public void DrawTileMap(SpriteBatch sb, Texture2D pixel)
        {
            for (int y = 0; y < 20; y++) {
                for (int x = 0; x < 50; x++) {
                    if (_map.IsSolid(x, y)) {
                        sb.Draw( pixel, 
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
        }
    }
}
