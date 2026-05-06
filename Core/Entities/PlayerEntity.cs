using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Core.Entities.Config;
using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class PlayerEntity : Entity
    {
        private MovementIntent _intent;

        //private Texture2D _whitePixel;

        private readonly IMovement _movement;
        private readonly VerticalMovement _vertical;
        private readonly PlayerEntityConfig _config;

        private int _health = 1;
        public bool IsDead { get; private set; } = false;
        public bool JustJumped { get; private set; } = false;

        private Dictionary<AnimationType, Animation> _animations;
        private Animation _currentAnimation;
        //private AnimationType _currentState;
        private int _frameIndex;
        private float _timer;

        public PlayerEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Player, EntityType.Mario)
        {
            _config = new PlayerEntityConfig();

            _movement = new PhysicsMovement(
                _config.MoveAcceleration,
                _config.MaxSpeed,
                _config.Friction,
                _config.AirControl
            );

            _vertical = new VerticalMovement(
                _config.Gravity,
                _config.JumpSpeed,
                _config.JumpCutMultiplier,
                _config.JumpBufferTime,
                _config.CoyoteTime
            );
        }

        public void SetAnimations(Dictionary<AnimationType, Animation> animations)
        {
            _animations = animations;
            SetAnimation(AnimationType.Idle); // стартовое состояние
        }

        private void SetAnimation(AnimationType type)
        {
            var newAnimation = _animations[type];

            if (_currentAnimation == newAnimation)
                return;

            _currentAnimation = newAnimation;
            _frameIndex = 0;
            _timer = 0;
        }

        public override void Update(float dt)
        {
            JustJumped = false;

            ReadInput();

            //_movement.Apply(this, _intent.DirectionX, 0, Contacts.IsGrounded, dt);
            _movement.Apply(this, _intent, Contacts.IsGrounded, dt);
            _vertical.UpdateTimers(_intent.JumpPressed, Contacts.IsGrounded, dt);

            JustJumped = _vertical.Apply(
                this,
                _intent.JumpPressed,
                _intent.JumpReleased,
                Contacts.IsGrounded,
                dt
            );

            UpdateFrame(dt);

            base.Update( dt );
        }

        private void UpdateFrame(float dt)
        {
            if (Velocity.X != 0)
                SetAnimation(AnimationType.Run);
            else
                SetAnimation(AnimationType.Idle);

            if(Velocity.Y != 0)
                SetAnimation(AnimationType.Jump);

                _timer += dt;

            if (_timer > _currentAnimation.FrameTime) {
                _frameIndex++;
                _timer = 0;

                if (_frameIndex >= _currentAnimation.Frames.Length)
                    _frameIndex = 0;
            }
        }

        private void ReadInput()
        {
            _intent.DirectionX = 0;

            if (Input.IsKeyDown(Keys.Left))
                _intent.DirectionX -= 1;

            if (Input.IsKeyDown(Keys.Right))
                _intent.DirectionX += 1;

            _intent.JumpPressed = Input.IsKeyPressed(Keys.Space);
            _intent.JumpReleased = Input.IsKeyReleased(Keys.Space);
        }

        public override void Draw(SpriteBatch sb, Texture2D texture)
        {
            var effect = Facing == -1  ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if (_currentAnimation != null) {

                var source = _currentAnimation.Frames[_frameIndex];

                sb.Draw(
                    _currentAnimation.Texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                    source,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    effect,
                    0f
                );
            }
            else {
                sb.Draw(
                    texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    effect,
                    0f
                );
            }
        }

        public override void TakeDamage()
        {
            _health--;

            if (_health <= 0) {
                Kill();
                _health = _config.Health;
            }
        }

        public void Kill()
        {
            if (IsDead) return;

            IsDead = true;
        }
    }
}
