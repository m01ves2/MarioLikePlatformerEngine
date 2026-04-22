using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Scenes
{
    public class SceneManager
    {
        private Scene _current;

        public void SetScene(Scene scene, GameResources resources)
        {
            _current = scene;
            _current?.Load(resources);
        }

        public void Update(float dt)
        {
            _current?.Update(dt);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _current?.Draw(spriteBatch);
        }
    }
}
