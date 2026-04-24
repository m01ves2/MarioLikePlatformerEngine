using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine.Core
{
    public class PlayerEntity : Entity
    {
        private Texture2D _whitePixel;
        private float _gravity = 800f;
        private float _speed = 200f;
        private float _jump_speed = 500;

        private float _jumpBufferTime = 0.1f;
        private float _jumpBufferCounter = 0f;

        private float _coyoteTime = 0.1f;
        private float _coyoteCounter = 0f;

        private float _moveAcceleration = 1500f;
        private float _maxSpeed = 200f;
        private float _friction = 2000f;

        private float _airControl = 0.5f; //сопротивление воздуха

        private float _jumpCutMultiplier = 0.5f; //параметр “обрезки прыжка”
        private bool _isJumping;

        private float _inputX;
        private bool _jumpPressed;
        private bool _jumpReleased;

        //private int _width;
        //private int _height;

        public PlayerEntity(Vector2 position, int width, int height) : base(position, width, height, EntityTag.Player)
        {
            //_width = width;
            //_height = height;
        }
        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            ReadInput(dt);
            UpdateJumpTimers(dt);
            ApplyJumpLogic();
            ApplyGravity(dt);
            ApplyMovement(dt);
        }

        private void ReadInput(float dt)
        {
            _inputX = 0;

            if (Input.IsKeyDown(Keys.Left))
                _inputX -= 1;

            if (Input.IsKeyDown(Keys.Right))
                _inputX += 1;

            _jumpPressed = Input.IsKeyPressed(Keys.Space);
            _jumpReleased = Input.IsKeyReleased(Keys.Space);
        }

        private void UpdateJumpTimers(float dt)
        {
            if (_jumpPressed)
                _jumpBufferCounter = _jumpBufferTime;

            _jumpBufferCounter -= dt;

            if (IsGrounded)
                _coyoteCounter = _coyoteTime;
            else
                _coyoteCounter -= dt;
        }

        private void ApplyJumpLogic()
        {
            if (_jumpBufferCounter > 0 && _coyoteCounter > 0) {
                Velocity.Y = -_jump_speed;
                _jumpBufferCounter = 0;
            }

            if (_jumpReleased && Velocity.Y < 0) {
                Velocity.Y *= _jumpCutMultiplier;
            }
        }

        private void ApplyGravity(float dt)
        {
            if (!IsGrounded)
                Velocity.Y += _gravity * dt;
        }

        private void ApplyMovement(float dt)
        {
            float control = IsGrounded ? 1f : _airControl;

            if (_inputX != 0) {
                Velocity.X += _inputX * _moveAcceleration * control * dt;
            }
            else {
                ApplyFriction(dt);
            }

            Velocity.X = MathHelper.Clamp(Velocity.X, -_maxSpeed, _maxSpeed);
        }

        private void ApplyFriction(float dt)
        {
            if (Velocity.X > 0) {
                Velocity.X -= _friction * dt;
                if (Velocity.X < 0) Velocity.X = 0;
            }
            else if (Velocity.X < 0) {
                Velocity.X += _friction * dt;
                if (Velocity.X > 0) Velocity.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // просто рисуем прямоугольник/текстуру
            spriteBatch.Draw(_whitePixel, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
        }
    }
}
