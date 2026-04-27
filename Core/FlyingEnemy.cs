using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Scenes;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core
{
    public class FlyingEnemy : Entity
    {
        private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly IBehavior _behavior;
        private TileMap _map;

        public FlyingEnemy(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Enemy)
        {
            //_movement = new PhysicsMovement(800f, 150f, 0f, 1f);
            _movement = new SimpleMovement();
            _behavior = new FlyingBehavior();
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            var intent = _behavior.GetIntent(this, _map, dt);

            _movement.Apply(this, intent, false, dt);
        }

        public void Sense(TileMap map)
        {
            _map = map;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _whitePixel,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                Color.Yellow
            );
        }
    }
}
