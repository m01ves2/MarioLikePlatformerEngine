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

        //    CastleTexture = content.Load<Texture2D>("castle");
        //    PrincessTexture = content.Load<Texture2D>("princess");
            };

        }


        //public Texture2D MarioTexture;
        //public Texture2D GoombaTexture; //бегает
        //public Texture2D Paratroopa; //летает
        ////public Texture2D Bowser; //Boss
        ////public Texture2D PiranhaPlant; //plant
        //public Texture2D CoinTexture;

        //public Texture2D GroundTexture;
        //public Texture2D StoneTexture;
        //public Texture2D BrickTexture;
        //public Texture2D QBlockTexture;
        //public Texture2D PipeTexture;

        //public Texture2D BackgroundTexture;
        //public Texture2D CastleTexture;
        //public Texture2D PrincessTexture;

        ////public SoundEffect HitSound;
        ////public SoundEffect BrickSound;
        ////public SoundEffect LoseSound;
        ////public SoundEffect JumpSound;

        //public void Load(ContentManager content)
        //{
        //    MarioTexture = content.Load<Texture2D>("mario");
        //    GoombaTexture = content.Load<Texture2D>("goomba");
        //    Paratroopa = content.Load<Texture2D>("paratroopa");
        //    CoinTexture = content.Load<Texture2D>("coin");

        //    GroundTexture = content.Load<Texture2D>("ground");
        //    StoneTexture = content.Load<Texture2D>("stone");
        //    BrickTexture = content.Load<Texture2D>("brick");
        //    QBlockTexture = content.Load<Texture2D>("qblock");
        //    PipeTexture = content.Load<Texture2D>("pipe");

        //    BackgroundTexture = content.Load<Texture2D>("background");
        //    CastleTexture = content.Load<Texture2D>("castle");
        //    PrincessTexture = content.Load<Texture2D>("princess");



        //    //HitSound = content.Load<SoundEffect>("hitSound");
        //    //BrickSound = content.Load<SoundEffect>("brickSound");
        //    //LoseSound = content.Load<SoundEffect>("loseSound");

        //}
    }
}
