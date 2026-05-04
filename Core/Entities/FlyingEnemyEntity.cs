using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Resources;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class FlyingEnemyEntity : Entity
    {
        private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly IBehavior _behavior;
        private TileMap _map;
        public bool WasKilled { get; set; }

        public FlyingEnemyEntity(Vector2 position, FlyingBehavior flyingBehavior, int width, int height)
            : base(position, width, height, EntityTag.Enemy, EntityType.Paratroopa)
        {
            //_movement = new PhysicsMovement(800f, 150f, 0f, 1f);
            _movement = new SimpleMovement();
            _behavior = flyingBehavior;
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            var intent = _behavior.GetIntent(this, _map, dt);

            _movement.Apply(this, intent, false, dt);

            base.Update(dt);
        }

        public void Sense(TileMap map)
        {
            _map = map;
        }
    }
}
