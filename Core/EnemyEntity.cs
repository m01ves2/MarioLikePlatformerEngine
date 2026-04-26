using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Components.Behavior;
using MarioLikePlatformerEngine.Core.Components.Movement;
using MarioLikePlatformerEngine.Core.Config;
using MarioLikePlatformerEngine.Scenes;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MarioLikePlatformerEngine.Core
{
    public class EnemyEntity : Entity
    {
        private Texture2D _whitePixel;
        private readonly IMovement _movement;
        private readonly EnemyEntityConfig _config;
        private readonly IBehavior _behavior;

        //private float _direction = 1;
        private TileMap _map;

        public EnemyEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Enemy)
        {
            _config = new EnemyEntityConfig();

            _movement = new SimpleMovement();

            _behavior = new PatrolBehavior();
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            float inputX = _behavior.GetInputX(this, _map, dt);
            _movement.Apply(this, inputX, Contacts.IsGrounded, dt);
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
                Color.Red // чтобы отличался от игрока
            );
        }
    }
}
