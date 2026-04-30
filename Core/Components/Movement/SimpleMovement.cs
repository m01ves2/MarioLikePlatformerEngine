using MarioLikePlatformerEngine.Core.Entities;

namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    class SimpleMovement : IMovement
    {
        public void Apply(Entity e, MovementIntent intent, bool grounded, float dt)
        {
            e.Velocity.X = intent.DirectionX * 50f;
            e.Velocity.Y = intent.DirectionY * 50f;
        }
    }
}
