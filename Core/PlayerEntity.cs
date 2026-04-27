using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Core.Config;
using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MarioLikePlatformerEngine.Core
{
    public class PlayerEntity : Entity
    {
        //private struct PlayerInput
        //{
        //    public float DirectionX;
        //    public bool JumpPressed;
        //    public bool JumpReleased;
        //}
        //private PlayerInput _input;

        private MovementIntent _intent;

        private Texture2D _whitePixel;

        //private float _inputX;
        //private bool _jumpPressed;
        //private bool _jumpReleased;

        private readonly IMovement _movement;
        private readonly VerticalMovement _vertical;
        private readonly PlayerEntityConfig _config;

        private int _health = 1;
        public bool IsDead { get; private set; } = false;


        public PlayerEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Player)
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

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        //public override void Update(float dt)
        //{
        //    ReadInput();

        //    _movement.Apply(this, _inputX, Contacts.IsGrounded, dt);

        //    _vertical.UpdateTimers(_jumpPressed, Contacts.IsGrounded, dt);
        //    _vertical.Apply(this, _jumpPressed, _jumpReleased, Contacts.IsGrounded, dt);
        //}

        //private void ReadInput()
        //{
        //    _inputX = 0;

        //    if (Input.IsKeyDown(Keys.Left))
        //        _inputX -= 1;

        //    if (Input.IsKeyDown(Keys.Right))
        //        _inputX += 1;

        //    _jumpPressed = Input.IsKeyPressed(Keys.Space);
        //    _jumpReleased = Input.IsKeyReleased(Keys.Space);
        //}

        public override void Update(float dt)
        {
            ReadInput();

            _movement.Apply(this, _intent.DirectionX, Contacts.IsGrounded, dt);

            _vertical.UpdateTimers(_intent.JumpPressed, Contacts.IsGrounded, dt);

            _vertical.Apply(
                this,
                _intent.JumpPressed,
                _intent.JumpReleased,
                Contacts.IsGrounded,
                dt
            );
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(
            //    _whitePixel,
            //    new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
            //    Color.White
            //);
            spriteBatch.Draw(
                _whitePixel,
                new Rectangle(
                    (int)MathF.Round(Position.X),
                    (int)MathF.Round(Position.Y),
                    Width,
                    Height),
                Color.White
            );
        }

        public override void TakeDamage()
        {
            _health--; 

            if(_health <=0 )
                Kill();
        }

        public void Kill()
        {
            if (IsDead) return;

            IsDead = true;
        }
    }
}
