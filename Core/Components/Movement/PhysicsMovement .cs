using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    public class PhysicsMovement : IMovement
    {
        private float _moveAcceleration;
        private float _maxSpeed;
        private float _friction;
        private float _airControl;

        public PhysicsMovement(float moveAcceleration, float maxSpeed, float friction, float airControl)
        {
            _moveAcceleration = moveAcceleration;
            _maxSpeed = maxSpeed;
            _friction = friction;
            _airControl = airControl;
        }

        public void Apply(Entity entity, float inputX, bool isGrounded, float dt)
        {
            float control = isGrounded ? 1f : _airControl;

            if (inputX != 0) {
                entity.Velocity.X += inputX * _moveAcceleration * control * dt;
            }
            else {
                ApplyFriction(entity, dt);
            }

            entity.Velocity.X = MathHelper.Clamp(entity.Velocity.X, -_maxSpeed, _maxSpeed);
        }

        private void ApplyFriction(Entity entity, float dt) //тренние
        {
            if (entity.Velocity.X > 0) {
                entity.Velocity.X -= _friction * dt;
                if (entity.Velocity.X < 0) entity.Velocity.X = 0;
            }
            else if (entity.Velocity.X < 0) {
                entity.Velocity.X += _friction * dt;
                if (entity.Velocity.X > 0) entity.Velocity.X = 0;
            }
        }
    }
}
