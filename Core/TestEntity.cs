using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine.Core
{
    class TestEntity : Entity
    {
        private Texture2D _whitePixel;
        private float _acceleration = 800f;
        private float _speed = 5f;
        private float _jump_speed = 1000;

        public TestEntity(Vector2 position) : base(position) { }
        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            //Position.X += 50 * dt;
            HandleMovement(dt);
            Velocity.Y += _acceleration * dt;
            Position += Velocity * dt;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // просто рисуем прямоугольник/текстуру
            spriteBatch.Draw(_whitePixel, new Rectangle((int)Position.X, (int)Position.Y, 20, 20), Color.White);
        }


        public void HandleMovement(float dt)
        {
            if (Input.IsKeyDown(Keys.Left)) {
                Velocity.X += -_speed;
                //Position.X -= _acceleration * dt;
            }
            if (Input.IsKeyDown(Keys.Right)) {
                Velocity.X += _speed;
                //Position.X += _acceleration * dt;
            }
            if (Input.IsKeyDown(Keys.Up)) {
                //Position.Y -= _acceleration * dt;
            }
            if (Input.IsKeyDown(Keys.Down)) {
                //Position.Y += _acceleration * dt;
            }

            if (Input.IsKeyPressed(Keys.Space)) {
                Velocity.Y -= _jump_speed;
            }
        }
    }
}
