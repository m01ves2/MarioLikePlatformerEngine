using MarioLikePlatformerEngine.World;

namespace MarioLikePlatformerEngine.Core
{
    public class GameContext
    {
        public GameState State;
        public GameCommand Command = GameCommand.None;
        public TileMap Map;
        public int Scores;
        public int Lives;
    }
}
