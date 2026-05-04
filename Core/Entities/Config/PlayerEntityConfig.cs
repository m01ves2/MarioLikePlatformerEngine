namespace MarioLikePlatformerEngine.Core.Entities.Config
{
    public class PlayerEntityConfig
    {
        // Movement
        public float MoveAcceleration = 1500f;
        public float MaxSpeed = 150f;
        public float Friction = 300f;
        public float AirControl = 0.5f;

        // Gravity / Jump
        public float Gravity = 1000f;
        public float JumpSpeed = 500f;
        public float JumpCutMultiplier = 0.5f;

        public float JumpBufferTime = 0.1f;
        public float CoyoteTime = 0.1f;

        public int Health = 1;
    }
}
