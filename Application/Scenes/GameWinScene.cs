using MarioLikePlatformerEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarioLikePlatformerEngine.Application.Scenes
{
    public class GameWinScene : Scene
    {
        private readonly GameResult _result;
        public GameWinScene(GameResult result)
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
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_resources.GameWinMusic);
        }

        public override SceneUpdateResult Update(float dt)
        {
            if (Input.IsKeyPressed(Keys.Space)) {
                return new SceneUpdateResult(GameCommand.GoToMenu, _result);
            }

            return SceneUpdateResult.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var x = _resources.ScreenWidth / 2 - 150;
            var y = _resources.ScreenHeight / 2 - 100;

            spriteBatch.Begin();
            DrawCentered(spriteBatch, $"Y O U  W O N !", y, Color.White);
            DrawCentered(spriteBatch, $"Scores: {_result.Score}", y + 50, Color.White);
            DrawCentered(spriteBatch, $"Press any key to continue", y + 100, Color.Gray);
            spriteBatch.End();
        }
    }
}
