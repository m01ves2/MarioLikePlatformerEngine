using MarioLikePlatformerEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine.Scenes
{
    public class TestScene : Scene
    {
        private Texture2D _whitePixel;
        private float _speed = 800f;
        private Vector2 _position = new Vector2(100, 100);

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_whitePixel, new Rectangle((int)_position.X, (int)_position.Y, 100, 100), Color.White);
        }


        public override void Update(float dt)
        {
            base.Update(dt);
            HandleMovement(dt);
        }

        public void HandleMovement(float dt)
        {
            if (Input.IsKeyDown(Keys.Left)){
                _position.X -= _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Right)) {
                _position.X += _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Up)) {
                _position.Y -= _speed * dt;
            }
            if (Input.IsKeyDown(Keys.Down)) {
                _position.Y += _speed * dt;
            }
        }
    }
}
