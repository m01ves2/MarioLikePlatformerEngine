using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MarioLikePlatformerEngine.Resources
{
    public class GameResources
    {
        public SpriteFont Font;
        public Song Music;
        public Texture2D WhitePixel;
        public int ScreenWidth;
        public int ScreenHeight;
        public float Scale = 1.5f;

        public void Load(ContentManager Content, Texture2D whitePixel, int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            WhitePixel = whitePixel;
            Font = Content.Load<SpriteFont>("font");
            Music = Content.Load<Song>("background_music");
        }
    }
}
