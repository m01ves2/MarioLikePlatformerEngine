using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarioLikePlatformerEngine.Application.Scenes
{
    public class MenuScene : Scene
    {
        private readonly GameResult _result;
        private readonly TextureProvider _textures;

        private string[] menuItems = new string[] {
            "New Game",
            //"Settings",
            "Exit"
        };
        private int _selectedIndex = 0;
        private bool _isConfirmed = false;

        public MenuScene(TextureProvider textures, GameResult result)
        {
            _result = result;
            _textures = textures;
        }
        public override void Initialize()
        {
            StartMusic();
        }
        public void StartMusic()
        {
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_resources.MenuMusic);
        }

        public override SceneUpdateResult Update(float dt)
        {
            HandleInput();

            if (_isConfirmed) {
                switch (_selectedIndex) {
                    case 0: return new SceneUpdateResult(GameCommand.StartGame, _result);
                    case 1: return new SceneUpdateResult(GameCommand.Quit, _result);
                }
            }

            return SceneUpdateResult.None;
        }

        public void HandleInput()
        {
            if (Input.IsKeyReleased(Keys.Enter)) {
                _isConfirmed = true;
            }
            else if (Input.IsKeyPressed(Keys.Down)) {
                _selectedIndex++;
                if (_selectedIndex > menuItems.Length - 1)
                    _selectedIndex = menuItems.Length - 1;
            }
            else if (Input.IsKeyPressed(Keys.Up)) {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DrawBackground(spriteBatch);
            DrawMenu(spriteBatch);
            DrawScores(spriteBatch);
            spriteBatch.End();
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            var texture = _textures.Get(BackgroundType.Menu);
            spriteBatch.Draw(texture, new Rectangle(0, 0, _resources.ScreenWidth, _resources.ScreenHeight), Color.White);
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            var x = _resources.ScreenWidth / 2 - 150;
            var y = _resources.ScreenHeight / 2 - 100;


            for (int i = 0; i < menuItems.Length; i++) {
                Color color = (i == _selectedIndex) ? Color.Yellow : Color.Gray;
                DrawCentered(spriteBatch, menuItems[i], y + 100 + i * 50, color);
            }
    
        }

        public void DrawScores(SpriteBatch spriteBatch)
        {
            DrawCentered(spriteBatch, $"Last scores: {_result.Score}", _resources.ScreenHeight - 150, Color.Gray);
        }
    }
}

