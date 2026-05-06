using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Core.Entities.Config;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class Goomba : BaseEnemy
    {
        //private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly EnemyEntityConfig _config;
        private readonly IBehavior _behavior;

        public Goomba(Vector2 position, int width, int height, TileMap map)
            : base(position, width, height, EntityTag.Enemy, EntityType.Goomba, map)
        {
            _config = new EnemyEntityConfig();

            //_movement = new SimpleMovement();
            _movement = new PhysicsMovement(
                moveAcceleration: _config.MoveAcceleration,
                maxSpeed: _config.MaxSpeed,
                friction: _config.Friction,
                airControl: _config.AirControl
            );

            _behavior = new PatrolBehavior();
        }

        public override void Update(float dt)
        {
            var intent = _behavior.GetIntent(this, _map, dt);

            _movement.Apply(this, intent, false, dt);

            base.Update(dt);
        }
    }
}
