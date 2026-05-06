using MarioLikePlatformerEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarioLikePlatformerEngine.Application.Scenes
{
    public class GameOverScene : Scene
    {
        private readonly GameResult _result;
        public GameOverScene(GameResult result)
        {
            _result = result;
        }
        public override void Initialize()
        {
            StartMusic();
        }
        public void StartMusic()
        {
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(_resources.GameOverMusic);
        }

        public override SceneUpdateResult Update(float dt)
        {
            if (Input.IsAnyKeyDown()) {
                return new SceneUpdateResult(GameCommand.GoToMenu, new GameResult());
            }

            return SceneUpdateResult.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var x = _resources.ScreenWidth / 2 - 150;
            var y = _resources.ScreenHeight / 2 - 100;

            spriteBatch.Begin();
            DrawCentered(spriteBatch, $"G A M E  O V E R!", y, Color.White);
            DrawCentered(spriteBatch, $"Scores: {_result.Scores}", y + 50, Color.White);
            DrawCentered(spriteBatch, $"Press any key to continue", y + 100, Color.Gray);
            spriteBatch.End();
        }
    }
}
