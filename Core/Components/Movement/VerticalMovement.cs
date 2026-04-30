using MarioLikePlatformerEngine.Core.Entities;

namespace MarioLikePlatformerEngine.Core.Components.Movement
{
    public class VerticalMovement
    {
        private float _gravity;
        private float _jumpSpeed;
        private float _jumpCutMultiplier;

        private float _jumpBufferTime;
        private float _coyoteTime;

        private float _jumpBufferCounter;
        private float _coyoteCounter;

        public VerticalMovement(
            float gravity,
            float jumpSpeed,
            float jumpCutMultiplier,
            float jumpBufferTime,
            float coyoteTime)
        {
            _gravity = gravity;
            _jumpSpeed = jumpSpeed;
            _jumpCutMultiplier = jumpCutMultiplier;
            _jumpBufferTime = jumpBufferTime;
            _coyoteTime = coyoteTime;
        }

        public void Apply(Entity e, bool jumpPressed, bool jumpReleased, bool isGrounded, float dt)
        {
            // buffer
            if (jumpPressed)
                _jumpBufferCounter = _jumpBufferTime;

            _jumpBufferCounter -= dt;

            // coyote
            if (isGrounded)
                _coyoteCounter = _coyoteTime;
            else
                _coyoteCounter -= dt;

            // jump
            if (_jumpBufferCounter > 0 && _coyoteCounter > 0) {
                e.Velocity.Y = -_jumpSpeed;
                _jumpBufferCounter = 0;
            }

            // jump cut
            if (jumpReleased && e.Velocity.Y < 0) {
                e.Velocity.Y *= _jumpCutMultiplier;
            }

            // gravity
            if (!isGrounded) {
                e.Velocity.Y += _gravity * dt;
            }
        }

        public void UpdateTimers(bool jumpPressed, bool isGrounded, float dt)
        {
            if (jumpPressed)
                _jumpBufferCounter = _jumpBufferTime;

            _jumpBufferCounter -= dt;

            if (isGrounded)
                _coyoteCounter = _coyoteTime;
            else
                _coyoteCounter -= dt;
        }
    }
}
