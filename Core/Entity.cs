using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core
{
    public class Entity
    {
        public Vector2 Position;
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
