using MarioLikePlatformerEngine.World;

namespace MarioLikePlatformerEngine.Core.Components.Behavior
{
    public interface IBehavior
    {
        MovementIntent GetIntent(Entity e, TileMap map, float dt);
    }
}
