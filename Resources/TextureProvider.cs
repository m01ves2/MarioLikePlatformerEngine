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
        private Texture2D _backgroundTrees;
        private Texture2D _backgroundSky;

        public TextureProvider(
            Dictionary<EntityType, Texture2D> entityTextures,
            Dictionary<TileType, Texture2D> tileTextures,
            Texture2D backgroundTrees, Texture2D backgroundSky)
        {
            _entityTextures = entityTextures;
            _tileTextures = tileTextures;
            _backgroundTrees = backgroundTrees;
            _backgroundSky = backgroundSky;
        }

        public Texture2D Get(Entity entity) => _entityTextures[entity.Type];

        public Texture2D Get(TileType tile) => _tileTextures[tile];

        public Texture2D GetBackgroundTrees() => _backgroundTrees;
        public Texture2D GetBackgroundSky() => _backgroundSky;
    }
}
