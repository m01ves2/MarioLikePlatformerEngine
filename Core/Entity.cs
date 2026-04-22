using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core
{
    public class Entity
    {
        protected Vector2 Position;
        protected Vector2 Velocity;

        public bool IsPendingDestroy = false;

        public Entity(Vector2 position) { 
            Position = position;
        }

        public virtual void Update(float dt) {
        }
        public virtual void Draw(SpriteBatch sb) { 
        }

        public virtual void Load(GameResources resources) { 
        }
    }
}
