using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Scenes
{
    public class LevelData
    {
        public TileMap Map;
        public PlayerEntity PlayerStart;
        public List<Entity> Enemies = new();
        public List<Entity> Coins = new();
        public Rectangle Goal;
    }
}
