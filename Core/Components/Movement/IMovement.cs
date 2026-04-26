namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    public interface IMovement
    {
        void Apply(Entity entity, float inputX, bool isGrounded, float dt);
    }
}
