using MarioLikePlatformerEngine.Application;
using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.Systems.Collisions;

namespace MarioLikePlatformerEngine.Systems.GameplayRules
{
    public class PlayerCoinCollectRule : ICollisionRule
    {
        public bool Matches(CollisionEvent e)
        {
            return (e.A.Tag == EntityTag.Player && e.B.Tag == EntityTag.Coin) ||
                   (e.A.Tag == EntityTag.Coin && e.B.Tag == EntityTag.Player);
        }

        public void Apply(CollisionEvent e, GameSession ctx)
        {
            var player = e.A.Tag == EntityTag.Player ? e.A : e.B;
            var coin = e.A.Tag == EntityTag.Coin ? e.A : e.B;

            coin.IsPendingDestroy = true;
            ctx.Scores += 100;
            (coin as CoinEntity).WasCollected = true;
        }

    }
}
