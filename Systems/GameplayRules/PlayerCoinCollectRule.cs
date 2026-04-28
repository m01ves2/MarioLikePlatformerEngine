using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Systems.Collisions;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.GameplayRules
{
    public class PlayerCoinCollectRule : ICollisionRule
    {
        public bool Matches(CollisionEvent e)
        {
            return (e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Coin);
        }

        public void Apply(CollisionEvent e)
        {
            var player = e.A as PlayerEntity;
            var coin = e.B;

            player.AddPoints(100);
            coin.IsPendingDestroy = true;
        }

    }
}
