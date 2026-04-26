using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.World;
using System;
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
        public void Step(List<Entity> entities, TileMap map, float dt, List<CollisionEvent> events)
        {
            // 1. reset
            foreach (var e in entities) {
                e.Contacts.IsGrounded = false;
                e.Contacts.IsTouchingWallLeft = false;
                e.Contacts.IsTouchingWallRight = false;
                e.Contacts.HitCeiling = false;
            }


            // 2. movement
            foreach (var e in entities)
                MoveX(e, entities, map, dt, events);

            foreach (var e in entities)
                MoveY(e, entities, map, dt, events);


            //foreach (var e in entities) {
            //    if (e.Contacts.IsGrounded) {
            //        if (Math.Abs(e.Velocity.Y) < 0.01f)
            //            e.Velocity.Y = 0;
            //    }
            //}
        }

        void MoveX(Entity e, List<Entity> entities, TileMap map, float dt, List<CollisionEvent> events)
        {
            e.Position.X += e.Velocity.X * dt;

            ResolveCollisions(e, entities, Axis.X, events);
            ResolveTileX(e, map);


        }

        void MoveY(Entity e, List<Entity> entities, TileMap map, float dt, List<CollisionEvent> events)
        {
            e.Position.Y += e.Velocity.Y * dt;

            ResolveCollisions(e, entities, Axis.Y, events);
            //ResolveTileY(e, map);


            //if (e.Contacts.IsGrounded) {
            //    if (Math.Abs(e.Velocity.Y) < 0.01f)
            //        e.Velocity.Y = 0;
            //}
            if (e.Velocity.Y != 0)
                ResolveTileY(e, map);

            CheckGroundSupport(e, map);
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

                        //if (!e.Bounds.Intersects(other.Bounds))
                        //    continue;
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
                //e.Contacts.IsTouchingWallLeft = true;
            }
            else if (side == CollisionSide.Right) {
                e.Position.X = other.Bounds.Right;
                e.Velocity.X = 0;
                //e.Contacts.IsTouchingWallRight = true;
            }
        }

        void ResolveY(Entity e, Entity other, CollisionSide side)
        {
            if (side == CollisionSide.Bottom) {
                e.Position.Y = other.Bounds.Top - e.Height;
                e.Velocity.Y = 0;
                //e.Contacts.IsGrounded = true;
            }
            else if (side == CollisionSide.Top) {
                e.Position.Y = other.Bounds.Bottom;
                e.Velocity.Y = 0;
                //e.Contacts.HitCeiling = true;
            }

            if (e.Contacts.IsGrounded) {
                e.Position.Y = MathF.Floor(e.Position.Y);
            }
        }

        void ResolveTileX(Entity e, TileMap map)
        {
            int dir = Math.Sign(e.Velocity.X);
            if (dir == 0) return;

            int left = (int)(e.Bounds.Left / map.TileSize);
            int right = (int)((e.Bounds.Right - 1) / map.TileSize);
            int top = (int)(e.Bounds.Top / map.TileSize);
            int bottom = (int)((e.Bounds.Bottom - 1) / map.TileSize);

            if (dir > 0) {
                for (int y = top; y <= bottom; y++) {
                    if (map.IsSolid(right, y)) {
                        e.Position.X = right * map.TileSize - e.Width;
                        e.Velocity.X = 0;
                        e.Contacts.IsTouchingWallRight = true;
                        break;
                    }
                }
            }
            else {
                for (int y = top; y <= bottom; y++) {
                    if (map.IsSolid(left, y)) {
                        e.Position.X = (left + 1) * map.TileSize;
                        e.Velocity.X = 0;
                        e.Contacts.IsTouchingWallLeft = true;
                        break;
                    }
                }
            }
        }

        void ResolveTileY(Entity e, TileMap map)
        {
            int dir = Math.Sign(e.Velocity.Y);
            if (dir != 0) {
                ResolveTileYMovement(e, map);
            }

            // И ВСЕГДА проверка опоры снизу
            CheckGroundSupport(e, map);

            //int left = (int)(e.Bounds.Left / map.TileSize);
            //int right = (int)((e.Bounds.Right - 1) / map.TileSize);
            //int top = (int)(e.Bounds.Top / map.TileSize);
            //int bottom = (int)((e.Bounds.Bottom - 1) / map.TileSize);

            //if (dir > 0) {
            //    for (int x = left; x <= right; x++) {
            //        int tileY = bottom;

            //        if (map.IsSolid(x, tileY)) {
            //            e.Position.Y = tileY * map.TileSize - e.Height;
            //            e.Velocity.Y = 0;
            //            e.Contacts.IsGrounded = true;
            //            break;
            //        }
            //    }
            //}
            //else {
            //    for (int x = left; x <= right; x++) {
            //        int tileY = top;

            //        if (map.IsSolid(x, tileY)) {
            //            e.Position.Y = (tileY + 1) * map.TileSize;
            //            e.Velocity.Y = 0;
            //            e.Contacts.HitCeiling = true;
            //            break;
            //        }
            //    }
            //}
        }


        void ResolveTileYMovement(Entity e, TileMap map)
        {
            int dir = Math.Sign(e.Velocity.Y);
            if (dir == 0) return;

            int left = (int)(e.Bounds.Left / map.TileSize);
            int right = (int)((e.Bounds.Right - 1) / map.TileSize);

            if (dir > 0) {
                int bottom = (int)((e.Bounds.Bottom - 1) / map.TileSize);

                for (int x = left; x <= right; x++) {
                    if (map.IsSolid(x, bottom)) {
                        e.Position.Y = bottom * map.TileSize - e.Height;
                        e.Velocity.Y = 0;
                        e.Contacts.IsGrounded = true;
                        return;
                    }
                }
            }
            else {
                int top = (int)(e.Bounds.Top / map.TileSize);

                for (int x = left; x <= right; x++) {
                    if (map.IsSolid(x, top)) {
                        e.Position.Y = (top + 1) * map.TileSize;
                        e.Velocity.Y = 0;
                        e.Contacts.HitCeiling = true;
                        return;
                    }
                }
            }
        }

        void CheckGroundSupport(Entity e, TileMap map)
        {
            int left = (int)(e.Bounds.Left / map.TileSize);
            int right = (int)((e.Bounds.Right - 1) / map.TileSize);
            int bottom = (int)((e.Bounds.Bottom) / map.TileSize);

            bool grounded = false;

            for (int x = left; x <= right; x++) {
                if (map.IsSolid(x, bottom)) {
                    grounded = true;

                    e.Position.Y = bottom * map.TileSize - e.Height;
                    break;
                }
            }

            e.Contacts.IsGrounded = grounded;

            if (grounded && Math.Abs(e.Velocity.Y) < 0.01f)
                e.Velocity.Y = 0;
        }
    }
}