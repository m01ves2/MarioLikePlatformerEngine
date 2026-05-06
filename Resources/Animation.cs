using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Resources
{
    public enum AnimationType
    {
        Idle,
        Run,
        Jump,
        Dying
    }

    public class Animation
    {
        public Texture2D Texture;
        public Rectangle[] Frames;
        public float FrameTime;
    }
}
