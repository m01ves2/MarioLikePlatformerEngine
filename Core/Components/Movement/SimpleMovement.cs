using MarioLikePlatformerEngine.Core.Entities;

namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    //class SimpleMovement : IMovement
    //{
    //    public void Apply(Entity e, float inputX, bool grounded, float dt)
    //    {
    //        e.Velocity.X = inputX * 100f;
    //        e.Position.X += e.Velocity.X * dt;
    //    }

    //    public void Apply(Entity e, float inputX, float inputY, bool isGrounded, float dt)
    //    {
    //        e.Velocity.X = inputX * 100f;
    //        e.Position.X += e.Velocity.X * dt;

    //        e.Velocity.Y = inputY * 100f;
    //        e.Position.Y += e.Velocity.Y * dt;
    //    }
    //}
    class SimpleMovement : IMovement
    {
        public void Apply(Entity e, MovementIntent intent, bool grounded, float dt)
        {
            e.Velocity.X = intent.DirectionX * 50f;
            e.Velocity.Y = intent.DirectionY * 50f;
        }
    }
}
