using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Application.Scenes
{
    public abstract class Scene
    {
        protected GameResources _resources;
        protected List<Entity> _entities;
        protected Camera2D _camera;

        //public abstract GameCommand Command { get; }
        public Scene()
        {
            _entities = new List<Entity>();
            _camera = new Camera2D();
        }

        public virtual void Load(GameResources resources)
        {
            _resources = resources;
        }

        public abstract void Initialize();

        public void AddEntity(Entity entity)
        {
            //entity.Load(_resources);
            _entities.Add(entity);
        }

        public virtual SceneUpdateResult Update(float dt) {

            foreach (Entity entity in _entities) {
                entity.Update(dt);
            }

            _entities.RemoveAll(e => e.IsPendingDestroy);

            return SceneUpdateResult.None;
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
        }

        protected void DrawCentered(SpriteBatch spriteBatch, string text, float y, Color color)
        {
            var size = _resources.Font.MeasureString(text);
            float x = _resources.ScreenWidth / 2f - size.X / 2f;

            spriteBatch.DrawString(_resources.Font, text, new Vector2(x, y), color);
        }
    }
}
