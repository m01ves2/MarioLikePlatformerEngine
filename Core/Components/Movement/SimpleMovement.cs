namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    class SimpleMovement : IMovement
    {
        public void Apply(Entity e, float inputX, bool grounded, float dt)
        {
            e.Velocity.X = inputX * 100f;
            e.Position.X += e.Velocity.X * dt;
        }
    }
}
