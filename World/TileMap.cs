namespace MarioLikePlatformerEngine.World
{
    public class TileMap
    {
        public int TileSize;

        private int[,] _tiles;

        public TileMap(int width, int height, int tileSize = 32)
        {
            TileSize = tileSize;
            _tiles = new int[height, width];
        }

        public bool IsSolid(int x, int y)
        {
            if (x < 0 || y < 0 || y >= _tiles.GetLength(0) || x >= _tiles.GetLength(1))
                return false;

            return _tiles[y, x] == 1;
            //1 = solid
            //0 = empty
        }

        public void SetSolid(int x, int y)
        {
            _tiles[y, x] = 1;
        }
    }
}
