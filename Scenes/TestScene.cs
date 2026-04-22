using MarioLikePlatformerEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Scenes
{
    public class TestScene : Scene
    {

        public TestScene() { }

        public override void Initialize()
        {
            AddEntity(new TestEntity( new Vector2( 100, 100)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
