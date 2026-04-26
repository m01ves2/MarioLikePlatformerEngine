using MarioLikePlatformerEngine.World;

namespace MarioLikePlatformerEngine.Core.Components.Behavior
{
    public interface IBehavior
    {
        //float GetInputX(Entity e, TileMap map);
        float GetInputX(Entity e, TileMap map, float dt);
    }
}
