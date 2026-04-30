using MarioLikePlatformerEngine.Core.Entities;

namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    public interface IMovement
    {
        void Apply(Entity entity, MovementIntent intent, bool isGrounded, float dt);
    }
}
