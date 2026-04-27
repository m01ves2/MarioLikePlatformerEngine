using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using System;

namespace MarioLikePlatformerEngine.Core.Components.Behavior
{
    public class FlyingBehavior : IBehavior
    {
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private Vector2 _direction = new Vector2(1, -1);

        public FlyingBehavior(float minX, float maxX, float minY, float maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        public MovementIntent GetIntent(Entity e, TileMap map, float dt)
        {
            // границы по X
            if (e.Position.X <= _minX)
                _direction.X = Math.Abs(_direction.X);

            if (e.Position.X + e.Width >= _maxX)
                _direction.X = -Math.Abs(_direction.X);

            // границы по Y
            if (e.Position.Y <= _minY)
                _direction.Y = Math.Abs(_direction.Y);

            if (e.Position.Y + e.Height >= _maxY)
                _direction.Y = -Math.Abs(_direction.Y);

            return new MovementIntent
            {
                DirectionX = _direction.X,
                DirectionY = _direction.Y
            };
        }
    }
}
