using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Resources
{
    public class GameAssets
    {
        public Dictionary<EntityType, Texture2D> EntityTextures;
        public Dictionary<TileType, Texture2D> TileTextures;
        public Texture2D BackgroundTreesTexture;
        public Texture2D BackgroundSkyTexture;
        public Texture2D GoalTexture;
        public Texture2D CastleTexture;

        public void Load(ContentManager content)
        {
            EntityTextures = new Dictionary<EntityType, Texture2D> 
            {
                { EntityType.Mario, content.Load<Texture2D>("mario") },
                { EntityType.Goomba, content.Load<Texture2D>("goomba") },
                { EntityType.Paratroopa, content.Load<Texture2D>("paratroopa") },
                { EntityType.PiranhaPlant, content.Load<Texture2D>("piranhaplant") },
                { EntityType.Bowser, content.Load<Texture2D>("bowser") },
                { EntityType.Coin, content.Load<Texture2D>("coin") },
            };

            TileTextures = new Dictionary<TileType, Texture2D>
            {
                { TileType.Ground, content.Load<Texture2D>("ground") },
                { TileType.Stone, content.Load<Texture2D>("stone") },
                { TileType.Brick, content.Load<Texture2D>("brick") },
                { TileType.QBlock, content.Load<Texture2D>("qblock") },
            };

            BackgroundTreesTexture = content.Load<Texture2D>("background_trees");
            BackgroundSkyTexture = content.Load<Texture2D>("background_sky");
            GoalTexture = content.Load<Texture2D>("princess");
            CastleTexture = content.Load<Texture2D>("castle");
        }
    }
}
