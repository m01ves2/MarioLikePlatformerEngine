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

            for(int i = 0; i < 10; i++) {
                AddEntity(new GroundEntity(new Vector2(i * 80, 550), 80, 50));
            }
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
