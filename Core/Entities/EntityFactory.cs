using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using System.Security.Principal;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class EntityFactory
    {
        private TextureProvider _textures;

        public EntityFactory(TextureProvider textures)
        {
            _textures = textures;
        }

        public PlayerEntity CreatePlayer(Vector2 position, int width, int height)
        {
            var player = new PlayerEntity(position, width, height);
            player.SetAnimations(_textures.Get(EntityType.Mario));
            return player;
        }

        public EnemyEntity CreateEnemy(Vector2 position, int width, int height)
        {
            var enemy = new EnemyEntity(position, width, height);
            enemy.SetAnimations(_textures.Get(EntityType.Goomba));
            return enemy;
        }

        public FlyingEnemyEntity CreateFlyingEnemy(Vector2 position, IBehavior behavior, int width, int height)
        {
            var enemy = new FlyingEnemyEntity(position, behavior, width, height);
            enemy.SetAnimations(_textures.Get(EntityType.Paratroopa));
            return enemy;
        }

        public CoinEntity CreateCoin(Vector2 position, int width, int height)
        {
            var coin = new CoinEntity(position, width, height);
            coin.SetAnimations(_textures.Get(EntityType.Coin));
            return coin;
        }
    }
}
