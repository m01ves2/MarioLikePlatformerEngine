using MarioLikePlatformerEngine.Core.Components;
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
        private readonly MovementComponent _movement;
        private readonly EnemyEntityConfig _config;

        private float _direction = 1;
        private TileMap _map;

        public EnemyEntity(Vector2 position, int width, int height)
            : base(position, width, height, EntityTag.Enemy)
        {
            _config = new EnemyEntityConfig();

            _movement = new MovementComponent(
                _config.MoveAcceleration,
                _config.MaxSpeed,
                _config.Friction,
                _config.AirControl
             );
        }

        public override void Load(GameResources resources)
        {
            _whitePixel = resources.WhitePixel;
        }

        public override void Update(float dt)
        {
            bool hasGroundAhead = CheckGroundAhead(dt);

            if (!hasGroundAhead || Contacts.IsTouchingWallLeft || Contacts.IsTouchingWallRight) {
                Velocity.X = 0;
                _direction *= -1;
            }

            float inputX = _direction;

            _movement.Apply(this, inputX, Contacts.IsGrounded, dt);
        }

        private bool CheckGroundAhead(float dt)
        {
            //Rectangle probe = new Rectangle((int)(Position.X + _direction * Width),
            //                                 (int)(Position.Y + Height + 1), Width, 2);

            //return _entities.Any(e => e.Tag == EntityTag.Ground && e.Bounds.Intersects(probe));

            //float nextX = Position.X + _direction * Width;

            //int footY = (int)((Position.Y + Height + 1) / _map.TileSize);
            //int checkX = (int)(nextX / _map.TileSize);

            //float nextX = Position.X + _direction * Width;

            float nextX = Position.X + (_direction > 0 ? Width : -1);
            int footY = (int)((Position.Y + Height + 1) / _map.TileSize);
            int checkX = (int)(nextX / _map.TileSize);

            return _map.IsSolid(checkX, footY);
        }

        public void Sense(TileMap map)
        {
            _map = map;
        }

        //private float GetAI()
        //{
        //    if (Contacts.IsTouchingWallLeft)
        //        _direction = 1;

        //    if (Contacts.IsTouchingWallRight)
        //        _direction = -1;

        //    return _direction;
        //}

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
