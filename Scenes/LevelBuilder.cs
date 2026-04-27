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
            var map = new TileMap(50, 20);
            PlayerEntity playerStart = null;
            var enemies = new List<Entity>();
            Rectangle goal = Rectangle.Empty;

            var level = new[]
            {
            "X................................................X",
            "X................................................X",
            "X.............XXX................................X",
            "X........................F.......................X",
            "X......XXX.......................XXXX............X",
            "X..............XXXX..............................X",
            "X......................................X.........X",
            "X...................xx...........................X",
            "X........XXX.................XXX.........X.......X",
            "X..................E.............................X",
            "X....F............XXXX.....................X.....X",
            "X................................................X",
            "X...................XXX.........XXX..............X",
            "X.........XXXX............................F......X",
            "X.....................E..........................X",
            "X....................XXXXXX......................X",
            "X...XXXX..............................X..........X",
            "X...........................X........XX..........X",
            "X.P................E.......XX.......XXX........G.X",
            "XXXXXXXXX...XXXXXXXXXXXXXXXXXXXX...XXXXXXXXXXXXXXX",
        };

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
                            playerStart = new PlayerEntity(new Vector2(x * 32 + (32 - widthP), y * 32 + +(32 - widthP)), widthP, heightP); //new Vector2(x * 32, y * 32);
                            break;

                        case 'E':
                            var widthE = 20;
                            var heightE = 20;
                            enemies.Add(new EnemyEntity(new Vector2(x * 32 + (32 - widthE), y * 32 + (32 - heightE)), widthE, heightE));
                            break;

                        case 'F':
                            var widthF = 20;
                            var heightF = 20;
                            var behavior = new FlyingBehavior(x * 32, x * 32 + 128, y * 32, y * 32 + 128);
                            enemies.Add(new FlyingEnemy(new Vector2(x * 32 + (32 - widthF), y * 32 + (32 - widthF) ), behavior, widthF, heightF));
                            break;

                        case 'G':
                            goal = new Rectangle(x * 32, y * 32, 32, 32);
                            break;
                    }
                }
            }


            return new LevelData() { Map = map, PlayerStart = playerStart, Enemies = enemies, Goal = goal };
        }
    }
}
