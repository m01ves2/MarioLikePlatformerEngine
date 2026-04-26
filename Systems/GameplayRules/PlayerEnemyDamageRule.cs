using MarioLikePlatformerEngine.Core;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class PlayerEnemyDamageRule : ICollisionRule //столкновение с противником сбоку = урон
    {
        public bool Matches(CollisionEvent e)
        {
            return     ((e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Enemy) || 
                        (e.A.Tag == EntityTag.Enemy && e.B.Tag == EntityTag.Player)) &&
                        (e.Side == CollisionSide.Left || e.Side == CollisionSide.Right);
        }

        public void Apply(CollisionEvent e)
        {
            var player = e.A.Tag == EntityTag.Player ? e.A : e.B;
            player.TakeDamage();
        }
    }
}
