using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Scenes
{
    public class Camera2D
    {
        public Vector2 Position;

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0));
        }

    }
}
