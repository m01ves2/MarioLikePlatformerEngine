using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Scenes
{
    public class TextureProvider
    {
        private Dictionary<EntityType, Texture2D> _entityTextures;
        private Dictionary<TileType, Texture2D> _tileTextures;

        public TextureProvider(
            Dictionary<EntityType, Texture2D> entityTextures,
            Dictionary<TileType, Texture2D> tileTextures)
        {
            _entityTextures = entityTextures;
            _tileTextures = tileTextures;
        }

        public Texture2D Get(Entity entity) => _entityTextures[entity.Type];

        public Texture2D Get(TileType tile) => _tileTextures[tile];
    }
}
