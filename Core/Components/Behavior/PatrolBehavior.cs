using MarioLikePlatformerEngine.World;

namespace MarioLikePlatformerEngine.Core.Components.Behavior
{
    public class PatrolBehavior : IBehavior
    {
        private float _direction = 1;
        public float GetInputX(Entity e, TileMap map, float dt)
        {
            bool hasGroundAhead = CheckGroundAhead(e, map, dt);

            if (!hasGroundAhead || e.Contacts.IsTouchingWallLeft || e.Contacts.IsTouchingWallRight) {
                _direction *= -1;
                return 0;
            }

            float inputX = _direction;

            return inputX;
        }

        private bool CheckGroundAhead(Entity e, TileMap map, float dt)
        {
            float nextX = e.Position.X + (_direction > 0 ? e.Width : -1);
            int footY = (int)((e.Position.Y + e.Height + 1) / map.TileSize);
            int checkX = (int)(nextX / map.TileSize);

            return map.IsSolid(checkX, footY);
        }
    }
}
