namespace MarioLikePlatformerEngine.World
{
    public class TileMap
    {
        public int TileSize;

        private int[,] _tiles;

        public int Width => _tiles.GetLength(1) * TileSize;
        public int Height => _tiles.GetLength(0) * TileSize;
        public int HeightInTiles => _tiles.GetLength(0);
        public int WidthInTiles => _tiles.GetLength(1);

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
