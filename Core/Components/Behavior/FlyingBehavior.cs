using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Components.Behavior
{
    public class FlyingBehavior : IBehavior
    {
        private Vector2 _direction = new Vector2(1, -1);

        public MovementIntent GetIntent(Entity e, TileMap map, float dt)
        {
            // отражение от стен
            if (e.Contacts.IsTouchingWallLeft || e.Contacts.IsTouchingWallRight)
                _direction.X *= -1;

            // отражение от пола/потолка
            if (e.Contacts.IsGrounded || e.Contacts.HitCeiling)
                _direction.Y *= -1;

            return new MovementIntent
            {
                DirectionX = _direction.X,
                DirectionY = _direction.Y
            };
        }
    }
}
