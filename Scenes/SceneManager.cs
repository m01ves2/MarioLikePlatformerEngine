using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Scenes
{
    public class SceneManager
    {
        private Scene _current;
        private GameResources _resources;

        public SceneManager(GameResources resources)
        {
            _resources = resources;
        }

        public void SetScene(Scene scene)
        {
            _current = scene;

            _current?.Load(_resources);
            _current.Initialize();
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
