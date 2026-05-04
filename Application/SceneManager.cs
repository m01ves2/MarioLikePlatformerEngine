using MarioLikePlatformerEngine.Application.Scenes;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Application
{
    public class SceneManager
    {
        private Scene _current;
        private GameResources _resources;
        private TextureProvider _textures;
        private SoundsProvider _sounds;
        private GameSettings _settings;
        private GameResult _gameResult;

        private bool _shouldExit = false;
        public bool ShouldExit => _shouldExit;

        //private Func<Scene> _sceneCreator;
        private Dictionary<GameCommand, Func<GameResult, Scene>> _sceneFactory;

        public SceneManager(GameResources resources, TextureProvider textures, SoundsProvider sounds, GameSettings settings)
        {
            _resources = resources;
            _textures = textures;
            _sounds = sounds;
            _settings = settings;

            _gameResult = new GameResult();
            //_sceneCreator = () => new GameScene(_textures, _sounds, _settings);

            //_current = _sceneCreator();
            _sceneFactory = new Dictionary<GameCommand, Func<GameResult, Scene>>()
            {
                { GameCommand.GoToMenu, (GameResult result) => new MenuScene(_textures, result) },
                { GameCommand.StartGame, (GameResult result) => new GameScene(_textures, _sounds, _settings) },
                { GameCommand.ShowGameOver, (GameResult result) => new GameOverScene(result) },
                { GameCommand.ShowGameWin, (GameResult result) => new GameWinScene(result) },
            };

            //_current = _sceneFactory[result.Command](result.Result);
        }

        public void SetScene(Scene scene)
        {
            _current = scene;

            _current?.Load(_resources);
            _current.Initialize();
        }   

        public void Update(float dt)
        {
            var updateResult = _current?.Update(dt);



            if (updateResult.Command == GameCommand.None) 
                return;

            if (updateResult.Command == GameCommand.Quit) {
                _shouldExit = true;
                return;
            }
            
            SetScene(_sceneFactory[updateResult.Command](updateResult.Result));
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _current?.Draw(spriteBatch);
        }
    }
}
