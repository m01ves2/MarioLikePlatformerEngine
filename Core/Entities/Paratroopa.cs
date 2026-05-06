using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class Paratroopa : BaseEnemy
    {
        //private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly IBehavior _behavior;

        public Paratroopa(Vector2 position, IBehavior flyingBehavior, int width, int height, TileMap map)
            : base(position, width, height, EntityTag.Enemy, EntityType.Paratroopa, map)
        {
            //_movement = new PhysicsMovement(800f, 150f, 0f, 1f);
            _movement = new SimpleMovement();
            _behavior = flyingBehavior;

            _isSpriteFacingRight = false; //у этого персонажа спрайт нарисован наоборот
        }

        public override void Update(float dt)
        {
            var intent = _behavior.GetIntent(this, _map, dt);

            _movement.Apply(this, intent, false, dt);

            base.Update(dt);
        }
    }
}
