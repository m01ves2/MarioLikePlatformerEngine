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
            float penetrationFromLeft = a.Right - b.Left;
            float penetrationFromRight = b.Right - a.Left;
            float penetrationFromTop = a.Bottom - b.Top;
            float penetrationFromBottom = b.Bottom - a.Top;

            float min = penetrationFromLeft;
            CollisionSide collisionSide = CollisionSide.Right;

            if (penetrationFromRight < min) {
                min = penetrationFromRight;
                collisionSide = CollisionSide.Left;
            }

            if (penetrationFromTop < min) {
                min = penetrationFromTop;
                collisionSide = CollisionSide.Bottom;
            }

            if (penetrationFromBottom < min) {
                min = penetrationFromBottom;
                collisionSide = CollisionSide.Top;
            }

            return collisionSide;
        }
    }
}
