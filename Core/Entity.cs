using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core
{
    public enum EntityTag //роль в взаимодействии
    {
        Player,
        Enemy,
        Ground
    }
    public class Entity
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public int Width;
        public int Height;
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        public EntityTag Tag { get; private set; }

        public bool IsPendingDestroy = false;
        public Contacts Contacts { get; set; } = new Contacts();

        public Entity(Vector2 position, int width, int height,  EntityTag tag) { 
            Position = position;
            Width = width;
            Height = height;
            Tag = tag;
        }

        public virtual void Update(float dt) {
        }
        public virtual void Draw(SpriteBatch sb) { 
        }

        public virtual void Load(GameResources resources) { 
        }

        public virtual void TakeDamage()
        {
            System.Diagnostics.Debug.WriteLine("ENTITY DAMAGED");
        }
    }
}
