using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Scenes
{
    public class Camera2D
    {
        public Vector2 Position;

        public Matrix GetViewMatrix(float scale, int screenWidth, int screenHeight)
        {
            return Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) * Matrix.CreateScale(scale);
        }
    }
}
