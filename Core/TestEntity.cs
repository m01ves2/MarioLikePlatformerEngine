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
        private float _speed = 800f;

        public TestEntity(Vector2 position) : base(position) { }
        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            //Position.X += 50 * dt;
            HandleMovement(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // просто рисуем прямоугольник/текстуру
            spriteBatch.Draw(_whitePixel, new Rectangle((int)Position.X, (int)Position.Y, 100, 100), Color.White);
        }


        public void HandleMovement(float dt)
        {
            if (Input.IsKeyDown(Keys.Left)) {
                Position.X -= _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Right)) {
                Position.X += _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Up)) {
                Position.Y -= _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Down)) {
                Position.Y += _speed * dt;
            }
        }
    }
}
