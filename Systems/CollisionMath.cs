using MarioLikePlatformerEngine.Systems.Collisions;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Systems
{
    public static class CollisionMath
    {
        public enum CollisionSide
        {
            Top,
            Bottom,
            Left,
            Right
        }

        public static CollisionSide DetectSide(Rectangle a, Rectangle b)
        {
            float penetrationFromRight = a.Right - b.Left;
            float penetrationFromLeft = b.Right - a.Left;
            float penetrationFromTop = a.Bottom - b.Top;
            float penetrationFromBottom = b.Bottom - a.Top;

            float min = penetrationFromLeft;
            CollisionSide collisionSide = CollisionSide.Right; // A пришёл справа

            if (penetrationFromRight < min) {
                min = penetrationFromRight;
                collisionSide = CollisionSide.Left; // A пришёл слева
            }

            if (penetrationFromTop < min) {
                min = penetrationFromTop;
                collisionSide = CollisionSide.Bottom; // A пришёл сверху
            }

            if (penetrationFromBottom < min) { // A пришёл снизу
                min = penetrationFromBottom;
                collisionSide = CollisionSide.Top;
            }

            return collisionSide;
        }
    }
}
