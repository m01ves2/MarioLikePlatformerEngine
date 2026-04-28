using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Scenes
{
    public static class LevelBuilder
    {
        public static LevelData CreateLevel()
        {
            var level = new[]
            {
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",   
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",
            "X..............................................................................X",
            "X...................................................................F..........X",
            "X..............................................................................X",
            "X.........................................................F....XXXX............X",
            "X................................E.............................................X",
            "X..............................XXXXX..................XXXX...............X.....X",
            "X......................................XXXXX.....E...............XXX....XX.....X",
            "X.......XXX................XXX..................XXXXX.................EXXX.....X",
            "X.....................................................................XXXX.....X",
            "XP.......E....X..............E........X..E.E..X....E...X....E..E.....XXXXX...G.X",
            "XXXXXXXXXXXXXXXXX.XXXX..XXXXXXXX..XXXXXXXXXXXXXXXXXXX.XXXXXXXXXXX..XXXXXXXXXXXXX",
        };

            int tileSize = 32;
            var map = new TileMap(level[0].Length, level.Length, tileSize);
            PlayerEntity playerStart = null;
            var enemies = new List<Entity>();
            Rectangle goal = Rectangle.Empty;

            for (int y = 0; y < level.Length; y++) {
                for (int x = 0; x < level[y].Length; x++) {
                    char c = level[y][x];

                    switch (c) {
                        case 'X':
                            map.SetSolid(x, y);
                            break;

                        case 'P':
                            var widthP = 20;
                            var heightP = 20;
                            playerStart = new PlayerEntity(new Vector2(x * tileSize + (tileSize - widthP), y * tileSize + (tileSize - widthP)), widthP, heightP); //new Vector2(x * tileSize, y * tileSize);
                            break;

                        case 'E':
                            var widthE = 20;
                            var heightE = 20;
                            enemies.Add(new EnemyEntity(new Vector2(x * tileSize + (tileSize - widthE), y * tileSize + (tileSize - heightE)), widthE, heightE));
                            break;

                        case 'F':
                            var widthF = 20;
                            var heightF = 20;
                            var behavior = new FlyingBehavior(x * tileSize, x * tileSize + 128, y * tileSize, y * tileSize + 128);
                            enemies.Add(new FlyingEnemy(new Vector2(x * tileSize + (tileSize - widthF), y * tileSize + (tileSize - widthF) ), behavior, widthF, heightF));
                            break;

                        case 'G':
                            goal = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            break;
                    }
                }
            }


            return new LevelData() { Map = map, PlayerStart = playerStart, Enemies = enemies, Goal = goal };
        }
    }
}
