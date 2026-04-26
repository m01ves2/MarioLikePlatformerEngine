using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Config;
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

        private float _inputX;
        private bool _jumpPressed;
        private bool _jumpReleased;

        private readonly MovementComponent _movement;
        private readonly VerticalMovementComponent _vertical;
        private readonly PlayerEntityConfig _config;

        public bool IsDead { get; set; } = false;

        public PlayerEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Player)
        {
            _config = new PlayerEntityConfig();

            _movement = new MovementComponent(
                _config.MoveAcceleration,
                _config.MaxSpeed,
                _config.Friction,
                _config.AirControl
            );

            _vertical = new VerticalMovementComponent(
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

        public override void Update(float dt)
        {
            ReadInput();

            _movement.Apply(this, _inputX, Contacts.IsGrounded, dt);

            _vertical.UpdateTimers(_jumpPressed, Contacts.IsGrounded, dt);
            _vertical.Apply(this, _jumpPressed, _jumpReleased, Contacts.IsGrounded, dt);
        }

        private void ReadInput()
        {
            _inputX = 0;

            if (Input.IsKeyDown(Keys.Left))
                _inputX -= 1;

            if (Input.IsKeyDown(Keys.Right))
                _inputX += 1;

            _jumpPressed = Input.IsKeyPressed(Keys.Space);
            _jumpReleased = Input.IsKeyReleased(Keys.Space);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _whitePixel,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                Color.White
            );
        }
    }
}
