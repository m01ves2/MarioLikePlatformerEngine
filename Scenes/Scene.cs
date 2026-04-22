using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Scenes
{
    public abstract class Scene
    {
        public virtual void Load(GameResources resources) { }
        public virtual void Update(float dt) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
