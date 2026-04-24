using MarioLikePlatformerEngine.Core;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class PlayerGroundRule : ICollisionRule
    {
        public bool Matches(CollisionEvent e)
        {
            return e.A.Tag == EntityTag.Player &&
                   e.B.Tag == EntityTag.Ground;
        }

        public void Apply(CollisionEvent e)
        {
            var player = e.A;
            var ground = e.B;

            switch (e.Side) {
                case CollisionSide.Bottom:
                    //// стоим на земле
                    //player.Velocity.Y = 0;
                    //player.Position.Y = ground.Bounds.Top - player.Height;
                    e.A.Contacts.IsGrounded = true;
                    break;

                case CollisionSide.Top:
                    //// удар головой
                    //player.Velocity.Y = 0;
                    //player.Position.Y = ground.Bounds.Bottom;
                    e.A.Contacts.HitCeiling = true;
                    break;

                case CollisionSide.Left:
                    //// упёрлись справа в стену
                    //player.Velocity.X = 0;
                    //player.Position.X = ground.Bounds.Right;
                    e.A.Contacts.IsTouchingWallLeft = true;
                    break;

                case CollisionSide.Right:
                    //// упёрлись слева
                    //player.Velocity.X = 0;
                    //player.Position.X = ground.Bounds.Left - player.Width;
                    e.A.Contacts.IsTouchingWallRight = true;
                    break;
            }
        }
    }
}
