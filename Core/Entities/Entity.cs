using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public enum EntityTag //роль в взаимодействии
    {
        Player,
        Enemy,
        Coin
    }

    public enum EntityType
    {
        Mario,

        Goomba,
        Paratroopa,
        PiranhaPlant,
        Bowser,

        Coin
    }

    public abstract class Entity
    {
        private static int _nextId = 0;
        public int Id { get; }

        public Vector2 Position;
        public Vector2 Velocity;

        public int Width;
        public int Height;

        public bool IsTrigger = false;
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        public EntityTag Tag { get; private set; }
        public EntityType Type { get; private set; }

        public bool IsPendingDestroy = false;
        public Contacts Contacts { get; set; } = new Contacts();

        public int Facing = 1; // 1 = вправо, -1 = влево

        protected Entity(Vector2 position, int width, int height,  EntityTag tag, EntityType type) { 
            Position = position;
            Width = width;
            Height = height;
            Tag = tag;
            Type = type;

            Id = _nextId++;
        }

        public virtual void Update(float dt) 
        {
            if (Velocity.X > 0)
                Facing = 1;
            else if (Velocity.X < 0)
                Facing = -1;
        }
        public virtual void Draw(SpriteBatch sb, Texture2D texture)
        {
            SpriteEffects effect = Facing == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            sb.Draw(
                texture,
                new Rectangle(
                    (int)MathF.Round(Position.X),
                    (int)MathF.Round(Position.Y),
                    Width,
                    Height),
                null,                // sourceRectangle (пока весь спрайт)
                Color.White,
                0f,                  // rotation
                Vector2.Zero,        // origin
                effect,
                0f                   // layerDepth
            );
        }

        public virtual void Load(GameResources resources) 
        {
        }

        public virtual void TakeDamage()
        {
            System.Diagnostics.Debug.WriteLine("ENTITY DAMAGED");
        }
    }
}
