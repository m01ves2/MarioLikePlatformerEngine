using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class CoinEntity : Entity
    {
        //private Texture2D _whitePixel;
        public bool WasCollected { get; set; }
        public CoinEntity(Vector2 position, int width, int height) : base(position, width, height, EntityTag.Coin, EntityType.Coin)
        {
            IsTrigger = true;
        }


        //public override void Load(GameResources resources)
        //{
        //    _whitePixel = resources.WhitePixel;
        //}

        //public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        //{
        //    spriteBatch.Draw(
        //        texture,
        //        new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
        //        Color.White
        //    );
        //}
    }
}
