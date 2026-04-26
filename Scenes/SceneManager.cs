using Microsoft.Xna.Framework.Graphics;
using System;

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

            if (_current.Command == Core.GameCommand.Restart) {
                RestartCurrentScene();
            }
        }

        private void RestartCurrentScene()
        {
            var type = _current.GetType();

            _current = (Scene)Activator.CreateInstance(type);

            _current.Load(_resources);
            _current.Initialize();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _current?.Draw(spriteBatch);
        }
    }
}
