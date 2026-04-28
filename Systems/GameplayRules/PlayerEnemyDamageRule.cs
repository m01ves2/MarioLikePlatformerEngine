using MarioLikePlatformerEngine.Core;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class PlayerEnemyDamageRule : ICollisionRule //столкновение с противником сбоку = урон
    {
        public bool Matches(CollisionEvent e)
        {
            //return     ((e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Enemy) || 
            //            (e.A.Tag == EntityTag.Enemy && e.B.Tag == EntityTag.Player)) &&
            //            (e.Side == CollisionSide.Left || e.Side == CollisionSide.Right);

            return      (e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Enemy)  &&
                        (e.Side == CollisionSide.Left || e.Side == CollisionSide.Right || e.Side == CollisionSide.Top);
        }

        public void Apply(CollisionEvent e, GameContext c)
        {
            //var player = e.A.Tag == EntityTag.Player ? e.A : e.B;
            //player.TakeDamage();

            var player = e.A;
            var enemy = e.B;
            player.TakeDamage();
        }
    }
}
