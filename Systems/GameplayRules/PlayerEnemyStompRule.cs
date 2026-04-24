using MarioLikePlatformerEngine.Core;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class PlayerEnemyStompRule : ICollisionRule //столкновение с противником сверху = stomp
    {
        public bool Matches(CollisionEvent e)
        {
            return e.A.Tag == EntityTag.Player &&
                   e.B.Tag == EntityTag.Enemy &&
                   e.Side == CollisionSide.Bottom;
        }

        public void Apply(CollisionEvent e)
        {
            var player = e.A;
            var enemy = e.B;

            player.Velocity.Y = -300f; // bounce
            enemy.IsPendingDestroy = true;
        }
    }
}
