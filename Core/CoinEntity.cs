using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core
{
    public class CoinEntity : Entity
    {
        private Texture2D _whitePixel;

        public CoinEntity(Vector2 position, int width, int height, EntityTag tag) : base(position, width, height, tag)
        {
            IsTrigger = true;
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _whitePixel,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                Color.Gold
            );
        }
    }
}
