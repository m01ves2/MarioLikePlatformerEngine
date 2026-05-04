namespace MarioLikePlatformerEngine.Application
{
    public class SceneUpdateResult
    {
        public GameCommand Command { get; }
        public GameResult Result { get; }

        public static readonly SceneUpdateResult None = new SceneUpdateResult(GameCommand.None, null);

        public SceneUpdateResult(GameCommand command, GameResult result)
        {
            Command = command;
            Result = result;
        }
    }
}
