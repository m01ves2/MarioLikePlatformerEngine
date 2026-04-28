namespace MarioLikePlatformerEngine.World
{
    public enum TileType
    {
        Empty = 0,
        Ground = 1,
        Grass = 2,
        Stone = 3
    }

    public class TileMap
    {
        public int TileSize;

        private TileType[,] _tiles;


        public TileType GetTile(int x, int y) => _tiles[y, x];
        public int Width => _tiles.GetLength(1) * TileSize;
        public int Height => _tiles.GetLength(0) * TileSize;
        public int HeightInTiles => _tiles.GetLength(0);
        public int WidthInTiles => _tiles.GetLength(1);

        public TileMap(int width, int height, int tileSize = 32)
        {
            TileSize = tileSize;
            _tiles = new TileType[height, width];
        }

        public bool IsSolid(int x, int y)
        {
            if (x < 0 || y < 0 || y >= _tiles.GetLength(0) || x >= _tiles.GetLength(1))
                return false;

            return _tiles[y, x] != TileType.Empty;
        }

        public void SetSolid(int x, int y, TileType tileType = TileType.Ground)
        {
            _tiles[y, x] = tileType;
        }
    }
}
