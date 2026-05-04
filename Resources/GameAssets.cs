using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Resources
{
    public enum SoundType
    {
        Coin,
        Jump,
        Kickkill
    }

    public enum BackgroundType
    {
        Trees,
        Sky,
        Goal,
        Castle,
        Menu
    }
    public class GameAssets
    {
        public Dictionary<EntityType, Texture2D> EntityTextures;
        public Dictionary<TileType, Texture2D> TileTextures;
        public Dictionary<BackgroundType, Texture2D> Backgrounds;

        public Dictionary<SoundType, SoundEffect> Sounds;

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

            Sounds = new Dictionary<SoundType, SoundEffect>
            {
                { SoundType.Coin, content.Load<SoundEffect>("coinSound") },
                { SoundType.Jump, content.Load<SoundEffect>("jumpSound") },
                { SoundType.Kickkill, content.Load<SoundEffect>("kickkillSound") },
            };

            Backgrounds = new Dictionary<BackgroundType, Texture2D>
            { 
                { BackgroundType.Trees, content.Load<Texture2D>("background_trees") },
                { BackgroundType.Sky, content.Load<Texture2D>("background_sky") },
                { BackgroundType.Goal, content.Load<Texture2D>("princess") },
                { BackgroundType.Castle, content.Load<Texture2D>("castle") },
                { BackgroundType.Menu, content.Load<Texture2D>("menu_background") },
            };
        }
    }
}
