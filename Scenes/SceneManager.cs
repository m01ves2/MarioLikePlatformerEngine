using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioLikePlatformerEngine.Scenes
{
    public class SceneManager
    {
        private Scene _current;
        private GameResources _resources;
        private TextureProvider _textures;
        private GameSettings _settings;

        private Func<Scene> _sceneCreator;      

        public SceneManager(GameResources resources, TextureProvider textures, GameSettings settings)
        {
            _resources = resources;
            _textures = textures;
            _settings = settings;

            _sceneCreator = () => new GameScene(_textures, _settings);
            _current = _sceneCreator();
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

            var cmd = _current?.ConsumeCommand();

            switch (cmd) {
                case GameCommand.Restart:
                    RestartCurrentScene();
                    break;
                case GameCommand.None:
                    break;
            }
        }

        private void RestartCurrentScene()
        {
            var type = _current.GetType();

            _current = _sceneCreator();

            _current.Load(_resources);
            _current.Initialize();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _current?.Draw(spriteBatch);
        }
    }
}
