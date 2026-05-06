using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected Dictionary<AnimationType, Animation> _animations;
        protected Animation _currentAnimation;
        protected int _frameIndex;
        protected float _timer;

        protected Entity(Vector2 position, int width, int height, EntityTag tag, EntityType type)
        {
            Position = position;
            Width = width;
            Height = height;
            Tag = tag;
            Type = type;

            Id = _nextId++;
        }


        public void SetAnimations(Dictionary<AnimationType, Animation> animations)
        {
            _animations = animations;
            _currentAnimation = animations.FirstOrDefault().Value;
        }

        public virtual void Update(float dt)
        {
            if (Velocity.X > 0)
                Facing = 1;
            else if (Velocity.X < 0)
                Facing = -1;

            SelectAnimation();
            UpdateAnimation(dt);
        }

        protected virtual void SelectAnimation()
        {

        }

        protected void SetAnimation(AnimationType type)
        {
            var newAnimation = _animations[type];

            if (_currentAnimation == newAnimation)
                return;

            _currentAnimation = newAnimation;
            _frameIndex = 0;
            _timer = 0;
        }

        protected void UpdateAnimation(float dt)
        {
            _timer += dt;

            if (_timer > _currentAnimation.FrameTime) {
                _frameIndex++;
                _timer = 0;

                if (_frameIndex >= _currentAnimation.Frames.Length)
                    _frameIndex = 0;
            }
        }


        public virtual void Draw(SpriteBatch sb)
        {
            var effect = Facing == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            var source = _currentAnimation.Frames[_frameIndex];

            sb.Draw(
                _currentAnimation.Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                source,
                Color.White,
                0f,
                Vector2.Zero,
                effect,
                0f
            );
        }

        public virtual void TakeDamage()
        {
            System.Diagnostics.Debug.WriteLine("ENTITY DAMAGED");
        }
    }
}
