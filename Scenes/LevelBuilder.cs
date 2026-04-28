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
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",   
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",
            "S..............................................................................S",
            "S...................................................................F..........S",
            "S..............................................................................S",
            "S.........................................................F....GGGG............S",
            "S................................E.............................................S",
            "S..............................GGGGG..................GGGG...............S.....S",
            "S......................................GGGGG.....E...............GGG....SS.....S",
            "S.......GGG................GGG.................GGGGG..................ESSS.....S",
            "S.....................................................................SSSS.....S",
            "SP.......E....X..............E........X..E.E..X....E...X....E..E.....SSSSS...W.S",
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
                        case 'X': //ground
                            map.SetSolid(x, y, TileType.Ground);
                            break;
                        case 'S': //stone
                            map.SetSolid(x, y, TileType.Stone);
                            break;
                        case 'G': //herb, grass
                            map.SetSolid(x, y, TileType.Grass);
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

                        case 'W':
                            goal = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            break;
                    }
                }
            }


            return new LevelData() { Map = map, PlayerStart = playerStart, Enemies = enemies, Goal = goal };
        }
    }
}
