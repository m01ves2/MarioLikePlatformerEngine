using MarioLikePlatformerEngine.Core;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Scenes
{
    public abstract class Scene
    {
        protected GameResources _resources;
        protected List<Entity> _entities;
        protected Camera2D _camera;
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
            entity.Load(_resources);
            _entities.Add(entity);
        }

        public virtual void Update(float dt) {

            foreach (Entity entity in _entities) {
                entity.Update(dt);
            }

            _entities.RemoveAll(e => e.IsPendingDestroy);
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
        }
    }
}
