namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    public interface IMovement
    {
        void Apply(Entity entity, float inputX, float inputY, bool isGrounded, float dt);
        //void Apply(Entity entity, float inputX, bool isGrounded, float dt);
        //void Apply(Entity entity, MovementIntent intent, bool isGrounded, float dt);
    }
}
