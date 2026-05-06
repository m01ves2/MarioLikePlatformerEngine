using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Resources;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class EntityFactory
    {
        private TextureProvider _textures;

        public EntityFactory(TextureProvider textures)
        {
            _textures = textures;
        }

        public Mario CreatePlayer(Vector2 position, int width, int height)
        {
            var player = new Mario(position, width, height);
            player.SetAnimations(_textures.Get(EntityType.Mario));
            return player;
        }

        public Goomba CreateEnemy(Vector2 position, int width, int height, TileMap map)
        {
            var enemy = new Goomba(position, width, height, map);
            enemy.SetAnimations(_textures.Get(EntityType.Goomba));
            return enemy;
        }

        public Paratroopa CreateFlyingEnemy(Vector2 position, IBehavior behavior, int width, int height, TileMap map)
        {
            var enemy = new Paratroopa(position, behavior, width, height, map);
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
