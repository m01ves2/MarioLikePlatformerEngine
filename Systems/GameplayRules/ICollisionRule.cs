using MarioLikePlatformerEngine.Application;
using MarioLikePlatformerEngine.Core;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    interface ICollisionRule //Интерфейс правила. правило само решает “подхожу ли я под эту ситуацию”
    {
        bool Matches(CollisionEvent e);
        void Apply(CollisionEvent e, GameSession ctx);
    }
}
