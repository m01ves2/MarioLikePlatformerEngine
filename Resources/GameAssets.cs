using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
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

        public Dictionary<EntityType, Dictionary<AnimationType, Animation>> Animations;

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

            Animations = new Dictionary<EntityType, Dictionary<AnimationType, Animation>>
            {
                { EntityType.Mario , new Dictionary<AnimationType, Animation>
                    {
                        { AnimationType.Idle, new Animation()
                            {
                                   Texture = content.Load<Texture2D>("sprite_sheet_mario"),
                                    Frames = new[]
                                    {
                                        new Rectangle(1, 8, 14, 15),
                                    },
                                    FrameTime = 0.1f
                            }
                        },

                        { AnimationType.Run, new Animation()
                            {
                                   Texture = content.Load<Texture2D>("sprite_sheet_mario"),
                                    Frames = new[]
                                    {
                                        new Rectangle(21, 8, 14, 15),
                                        new Rectangle(39, 8, 14, 15),
                                        new Rectangle(57, 8, 14, 15),
                                    },
                                    FrameTime = 0.1f
                            }
                        },

                        { AnimationType.Jump, new Animation()
                            {
                                   Texture = content.Load<Texture2D>("sprite_sheet_mario"),
                                    Frames = new[]
                                    {
                                        new Rectangle(97, 8, 14, 15),
                                    },
                                    FrameTime = 0.2f
                            }
                        },
                    }
                },

                //{ EntityType.Goomba, new Dictionary<AnimationType, Animation>
                //    {
                //        { AnimationType.Run, new Animation()
                //            {
                //                   Texture = content.Load<Texture2D>("sprite_sheet_enemies"),
                //                    Frames = new[]
                //                    {
                //                        new Rectangle(0, 16, 15, 15),
                //                        new Rectangle(18, 16, 15, 15),
                //                    },
                //                    FrameTime = 0.1f
                //            }
                //        },

                //    { AnimationType.Dying, new Animation()
                //            {
                //                   Texture = content.Load<Texture2D>("sprite_sheet_enemies"),
                //                    Frames = new[]
                //                    {
                //                        new Rectangle(36, 24, 15, 7),
                //                    },
                //                    FrameTime = 0.1f
                //            }
                //        },
                //    }

                //},

                //{ EntityType.Paratroopa, new Dictionary<AnimationType, Animation>
                //    {
                //        { AnimationType.Run, new Animation()
                //            {
                //                   Texture = content.Load<Texture2D>("sprite_sheet_enemies"),
                //                    Frames = new[]
                //                    {
                //                        new Rectangle(36, 112, 15, 23),
                //                        new Rectangle(54, 112, 15, 23),
                //                    },
                //                    FrameTime = 0.1f
                //            }
                //        },
                //    }

                //},

                //{ EntityType.Coin, new Dictionary<AnimationType, Animation>
                //    {
                //        { AnimationType.Idle, new Animation()
                //            {
                //                   Texture = content.Load<Texture2D>("SMB_Sprite_Coin"),
                //                    Frames = new[]
                //                    {
                //                        new Rectangle(0, 0, 9, 13),
                //                    },
                //                    FrameTime = 0.1f
                //            }
                //        },
                //    }

                //}
            };
        }
    }
}
