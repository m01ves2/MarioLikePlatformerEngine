using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public abstract class BaseEnemy : Entity
    {
        public bool WasKilled;
        protected TileMap _map;

        protected BaseEnemy(Vector2 position, int width, int height, EntityTag tag, EntityType type, TileMap map) : 
            base(position, width, height, tag, type)
        {
            _map = map;
        }

        //public virtual void OnKilled()
        //{
        //    WasKilled = true;
        //    IsPendingDestroy = true;
        //}
    }
}
