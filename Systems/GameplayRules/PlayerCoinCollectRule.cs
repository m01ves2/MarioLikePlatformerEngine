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

        public void Apply(CollisionEvent e, GameContext ctx)
        {
            var player = e.A;
            var coin = e.B;

            coin.IsPendingDestroy = true;
            ctx.Scores += 100;
        }

    }
}
