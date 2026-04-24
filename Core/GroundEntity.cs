using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine.Core
{
    public class GroundEntity : Entity
    {
        //private int _width;
        //private int _height;
        private Texture2D _whitePixel;

        public GroundEntity(Vector2 position, int width, int height) : base(position, width, height, EntityTag.Ground)
        {
            //_width = width;
            //_height = height;
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_whitePixel, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.Brown);
        }

    }
}
