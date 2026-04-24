using MarioLikePlatformerEngine.Core;
using System.Collections.Generic;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Physics
{
    public enum Axis
    {
        X,
        Y
    }

    public class PhysicsSystem
    {
        public void Step(List<Entity> entities, float dt, List<CollisionEvent> events)
        {
            foreach (var e in entities)
                MoveX(e, entities, dt, events);

            foreach (var e in entities)
                MoveY(e, entities, dt, events);
        }

        void MoveX(Entity e, List<Entity> entities, float dt, List<CollisionEvent> events)
        {
            e.Position.X += e.Velocity.X * dt;

            ResolveCollisions(e, entities, Axis.X, events);
        }

        void MoveY(Entity e, List<Entity> entities, float dt, List<CollisionEvent> events)
        {
            e.Position.Y += e.Velocity.Y * dt;

            ResolveCollisions(e, entities, Axis.Y, events);
        }

        void ResolveCollisions(Entity e, List<Entity> entities, Axis axis, List<CollisionEvent> events)
        {
            foreach (var other in entities) {
                if (other == e) continue;
                if (!e.Bounds.Intersects(other.Bounds)) continue;

                var side = DetectSide(e.Bounds, other.Bounds);

                events.Add(new CollisionEvent
                {
                    A = e,
                    B = other,
                    Side = side
                });


                // физика (раздвижение) — отдельно
                switch (axis) {
                    case Axis.X:
                        if (side == CollisionSide.Left || side == CollisionSide.Right)
                            ResolveX(e, other, side);
                        break;

                    case Axis.Y:
                        if (side == CollisionSide.Top || side == CollisionSide.Bottom)
                            ResolveY(e, other, side);
                        break;
                }
            }
        }

        void ResolveX(Entity e, Entity other, CollisionSide side)
        {
            if (side == CollisionSide.Left) {
                e.Position.X = other.Bounds.Left - e.Width;
                e.Velocity.X = 0;
            }
            else if (side == CollisionSide.Right) {
                e.Position.X = other.Bounds.Right;
                e.Velocity.X = 0;
            }
        }

        void ResolveY(Entity e, Entity other, CollisionSide side)
        {
            if (side == CollisionSide.Bottom) {
                e.Position.Y = other.Bounds.Top - e.Height;
                e.Velocity.Y = 0;
            }
            else if (side == CollisionSide.Top) {
                e.Position.Y = other.Bounds.Bottom;
                e.Velocity.Y = 0;
            }
        }
    }
}
