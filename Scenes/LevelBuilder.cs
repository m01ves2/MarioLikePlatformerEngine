using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Entities;
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
            "S................................................................C......C......S",
            "S..........................................C...............F...GGGG............S",
            "S................................E........C....................................S",
            "S..............................GGGGG.....C............GGGG...............S.....S",
            "S...........................C..........GGGGG.....E...............GGG....SS.....S",
            "S.......GGG................GGG.................GGGGG..................ESSS.....S",
            "S.............................................C.......................SSSS.....S",
            "SP.......E....X..............E........X..E.E..X....E...X..C.E..E....CSSSSS...W.S",
            "XXXXXXXXXXXXXXXXXX.XXX..XXXXXXX..XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXX..XXXXXXXXXXXXX",
        };

            int tileSize = 32;
            var map = new TileMap(level[0].Length, level.Length, tileSize);
            PlayerEntity playerStart = null;
            var enemies = new List<Entity>();
            var coins = new List<Entity>();
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
                            map.SetSolid(x, y, TileType.Brick);
                            break;

                        case 'C': //coin
                            var widthC = 20;
                            var heightC = 20;
                            coins.Add(new CoinEntity(new Vector2(x * tileSize + (tileSize - widthC), y * tileSize + +(tileSize - heightC)), widthC, heightC));
                            break;

                        case 'P':
                            var widthP = 20;
                            var heightP = 30;
                            playerStart = new PlayerEntity(new Vector2(x * tileSize + (tileSize - widthP), y * tileSize + (tileSize - heightP)), widthP, heightP); //new Vector2(x * tileSize, y * tileSize);
                            break;

                        case 'E':
                            var widthE = 20;
                            var heightE = 20;
                            enemies.Add(new EnemyEntity(new Vector2(x * tileSize + (tileSize - widthE), y * tileSize + (tileSize - heightE)), widthE, heightE));
                            break;

                        case 'F':
                            var widthF = 32;
                            var heightF = 46;
                            var behavior = new FlyingBehavior(x * tileSize, x * tileSize + 128, y * tileSize, y * tileSize + 128);
                            enemies.Add(new FlyingEnemyEntity(new Vector2(x * tileSize + (tileSize - widthF), y * tileSize + (tileSize - widthF) ), behavior, widthF, heightF));
                            break;

                        case 'W':
                            goal = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            break;
                    }
                }
            }


            return new LevelData() { Map = map, PlayerStart = playerStart, Enemies = enemies, Coins = coins,  Goal = goal };
        }
    }
}
