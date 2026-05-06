using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Resources
{
    public class TextureProvider
    {
        private Dictionary<EntityType, Texture2D> _entityTextures;
        private Dictionary<TileType, Texture2D> _tileTextures;
        Dictionary<BackgroundType, Texture2D> _backgroundTextures;

        Dictionary<EntityType, Dictionary<AnimationType, Animation>> _animations;

        public TextureProvider(
            Dictionary<EntityType, Texture2D> entityTextures,
            Dictionary<TileType, Texture2D> tileTextures,
            Dictionary<BackgroundType, Texture2D> backgroundTextures,
            Dictionary<EntityType, Dictionary<AnimationType, Animation>> animations)
        {
            _entityTextures = entityTextures;
            _tileTextures = tileTextures;
            _backgroundTextures = backgroundTextures;
            _animations = animations;
        }

        public Texture2D Get(Entity entity) => _entityTextures[entity.Type];

        public Texture2D Get(TileType tile) => _tileTextures[tile];

        public Texture2D Get(BackgroundType type) => _backgroundTextures[type];

        public Dictionary<AnimationType, Animation> Get(EntityType entityType) => _animations[entityType];
    }
}
