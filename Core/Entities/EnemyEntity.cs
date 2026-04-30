using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Core.Config;
using MarioLikePlatformerEngine.Resources;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class EnemyEntity : Entity
    {
        private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly EnemyEntityConfig _config;
        private readonly IBehavior _behavior;

        private TileMap _map;

        public EnemyEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Enemy, EntityType.Goomba)
        {
            _config = new EnemyEntityConfig();

            //_movement = new SimpleMovement();
            _movement = new PhysicsMovement(
                moveAcceleration: 9999f,
                maxSpeed: 100f,
                friction: 9999f,
                airControl: 1f
            );

            _behavior = new PatrolBehavior();
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

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                Color.White // чтобы отличался от игрока
            );
        }
    }
}
