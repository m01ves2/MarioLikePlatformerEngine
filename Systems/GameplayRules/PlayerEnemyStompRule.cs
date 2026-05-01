using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Entities;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class PlayerEnemyStompRule : ICollisionRule //столкновение с противником сверху = stomp
    {
        public bool Matches(CollisionEvent e)
        {
            return ((e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Enemy) ||
                   (e.A.Tag == EntityTag.Enemy && e.B.Tag == EntityTag.Player)) &&
                   ((e.A.Tag == EntityTag.Player && e.Side == CollisionSide.Bottom) ||
                   (e.A.Tag == EntityTag.Enemy && e.Side == CollisionSide.Top));
        }

        public void Apply(CollisionEvent e, GameContext c)
        {
            var player = e.A.Tag == EntityTag.Player? e.A : e.B;
            var enemy = e.A.Tag == EntityTag.Enemy ? e.A : e.B;

            player.Velocity.Y = -300f; // bounce
            enemy.IsPendingDestroy = true;
            (enemy as EnemyEntity).WasKilled = true;
        }
    }
}
