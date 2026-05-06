using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;

namespace MarioLikePlatformerEngine.Core.Entities
{
    public class FlyingEnemyEntity : Entity
    {
        //private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly IBehavior _behavior;
        private TileMap _map;
        public bool WasKilled { get; set; }

        public FlyingEnemyEntity(Vector2 position, IBehavior flyingBehavior, int width, int height)
            : base(position, width, height, EntityTag.Enemy, EntityType.Paratroopa)
        {
            //_movement = new PhysicsMovement(800f, 150f, 0f, 1f);
            _movement = new SimpleMovement();
            _behavior = flyingBehavior;
        }

        protected override void SelectAnimation()
        {
        }

        public override void Update(float dt)
        {
            var intent = _behavior.GetIntent(this, _map, dt);

            _movement.Apply(this, intent, false, dt);

            base.Update(dt);

            Facing = -Facing; //костыль, текстура в оригинальном spritesheet перевернута
        }

        public void Sense(TileMap map)
        {
            _map = map;
        }
    }
}
