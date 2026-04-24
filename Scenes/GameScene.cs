using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Systems.Collisions;
using MarioLikePlatformerEngine.Systems.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Scenes
{
    public class GameScene : Scene
    {
        private CollisionRulesSystem _rules;
        private PhysicsSystem _physics;
        public GameScene()
        {
            _rules = new CollisionRulesSystem();

            _rules.AddRule(new PlayerGroundRule());
            _rules.AddRule(new PlayerEnemyStompRule());
            _rules.AddRule(new PlayerEnemyDamageRule());

            _physics = new PhysicsSystem();
        }

        public override void Initialize()
        {
            AddEntity(new PlayerEntity(new Vector2(100, 100), 20, 20));

            for (int i = 0; i < 10; i++) {
                if (i == 3) continue;
                AddEntity(new GroundEntity(new Vector2(i * 80, 550), 80, 50));
            }

            AddEntity(new GroundEntity(new Vector2(80, 450), 80, 50));
            AddEntity(new GroundEntity(new Vector2(160, 450), 80, 50));

            AddEntity(new GroundEntity(new Vector2(480, 500), 80, 50));
            AddEntity(new GroundEntity(new Vector2(560, 500), 80, 50));
            AddEntity(new GroundEntity(new Vector2(560, 450), 80, 50));
            AddEntity(new GroundEntity(new Vector2(640, 500), 80, 50));
            AddEntity(new GroundEntity(new Vector2(720, 500), 80, 50));
            
            AddEntity(new GroundEntity(new Vector2(720, 420), 80, 50));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(float dt)
        {
            // 1. input / AI
            foreach (var e in _entities)
                e.Update(dt); // ТОЛЬКО input / AI

            // 2. physics + collisions
            List<CollisionEvent> events = new List<CollisionEvent>();
            _physics.Step(_entities, dt, events);

            _rules.Apply(events);
        }
    }
}
