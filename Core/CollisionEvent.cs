using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Core
{
    public struct CollisionEvent
    {
        public Entity A;
        public Entity B;
        public CollisionSide Side; // Top, Bottom, Left, Right - это сторона участника A
    }
}
