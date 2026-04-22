using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine.Inputs
{
    public class Input
    {
        public static KeyboardState Current;
        public static KeyboardState Previous;

        public static void Update()
        {
            Previous = Current;
            Current = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return Current.IsKeyDown(key);
        }

        public static bool IsKeyPress(Keys key)
        {
            return Current.IsKeyDown(key) && Previous.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return Current.IsKeyUp(key) && Previous.IsKeyDown(key);
        }
    }
}
