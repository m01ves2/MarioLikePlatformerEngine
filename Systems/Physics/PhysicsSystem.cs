using MarioLikePlatformerEngine.Core;
using MarioLikePlatformerEngine.Core.Components;
using MarioLikePlatformerEngine.Core.Entities;
using MarioLikePlatformerEngine.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static MarioLikePlatformerEngine.Systems.CollisionMath;

namespace MarioLikePlatformerEngine.Systems.Physics
{
    public struct PhysicsState
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Contacts Contacts;
    }

    public class PhysicsSystem
    {
        public void Step(List<Entity> entities, TileMap map, float dt, List<CollisionEvent> events)
        {
            foreach (var e in entities) {
                var state = new PhysicsState
                {
                    Position = e.Position,
                    Velocity = e.Velocity,
                    Contacts = new Contacts()
                };

                ResolveTiles(ref state, e, map, dt); //Position correction

                ResolveEntities(ref state, entities, e, events); //Velocity correction only

                // Recalculates ground contact using tiles only.
                // Overrides IsGrounded previously set by entity collisions.
                BuildContacts(ref state, e, map);

                Commit(e, state);
            }
        }

        private void ResolveTiles(ref PhysicsState state, Entity self, TileMap map, float dt)
        {
            // здесь используется раздельная система разрешения коллизий по горизонтали и вертикали
            // например - если Mario прыгнул и наскочил на вертикальную стену... 
            // Упрощённая схема: горизонтальное движение блокируется стеной, а вертикальное продолжается под действием гравитации.
            // это простейшая система разрешения, но есть и другие - существуют алгоритмы, которые решают такую задачу.
            // Например, можно использовать предыдущую позицию, глубину проникновения или вычислять время первого контакта.
            // Но они сложнее, чем наша система раздельного разрешения.

            // И всё же наша система не идеальна
            // Есть два ограничения:
            // Первое — порядок осей имеет значение. Мы всегда обрабатываем X, затем Y.В некоторых ситуациях на углах платформ это может давать результат, отличный от обработки Y, затем X.
            // Второе — возможен tunneling(проскок сквозь препятствие).
            // Например, при достаточно большой скорости объект за один кадр может переместиться целиком за тонкую стену. Наш алгоритм проверяет пересечение в конечной позиции, а не весь пройденный путь.
            // Для небольшого платформера с умеренными скоростями это допустимое упрощение.

            // X отдельно
            state.Position.X += state.Velocity.X * dt;
            ResolveTileX(ref state, self, map);

            // Y отдельно
            state.Position.Y += state.Velocity.Y * dt;
            ResolveTileY(ref state, self, map);
        }

        private void ResolveTileX(ref PhysicsState state, Entity self, TileMap map)
        {
            int dir = Math.Sign(state.Velocity.X);
            if (dir == 0) return;

            int left = (int)(state.Position.X / map.TileSize);
            int right = (int)((state.Position.X + self.Width - 1) / map.TileSize);
            int top = (int)(state.Position.Y / map.TileSize);
            int bottom = (int)((state.Position.Y + self.Height - 1) / map.TileSize);

            if (dir > 0) {
                for (int y = top; y <= bottom; y++) {
                    if (map.IsSolid(right, y)) {
                        state.Position.X = right * map.TileSize - self.Width;
                        state.Velocity.X = 0;
                        state.Contacts.IsTouchingWallRight = true;
                        return;
                    }
                }
            }
            else {
                for (int y = top; y <= bottom; y++) {
                    if (map.IsSolid(left, y)) {
                        state.Position.X = (left + 1) * map.TileSize;
                        state.Velocity.X = 0;
                        state.Contacts.IsTouchingWallLeft = true;
                        return;
                    }
                }
            }
        }
        private void ResolveTileY(ref PhysicsState state, Entity self, TileMap map)
        {
            int dir = Math.Sign(state.Velocity.Y);

            if (dir == 0) return;

            int left = (int)(state.Position.X / map.TileSize);
            int right = (int)((state.Position.X + self.Width - 1) / map.TileSize);

            if (dir > 0) {
                int bottom = (int)((state.Position.Y + self.Height) / map.TileSize);

                for (int x = left; x <= right; x++) {
                    if (map.IsSolid(x, bottom)) {
                        state.Position.Y = bottom * map.TileSize - self.Height;
                        state.Velocity.Y = 0;
                        state.Contacts.IsGrounded = true;
                        return;
                    }
                }
            }
            else {
                int top = (int)(state.Position.Y / map.TileSize);

                for (int x = left; x <= right; x++) {
                    if (map.IsSolid(x, top)) {
                        state.Position.Y = (top + 1) * map.TileSize;
                        state.Velocity.Y = 0;
                        state.Contacts.HitCeiling = true;
                        return;
                    }
                }
            }
        }

        private void ResolveEntities(ref PhysicsState state, List<Entity> entities, Entity self, List<CollisionEvent> events)
        {
            foreach (var other in entities) {
                if (other == self) continue;

                var selfBounds = new Rectangle(
                    (int)state.Position.X,
                    (int)state.Position.Y,
                    self.Width,
                    self.Height
                );

                var otherBounds = new Rectangle(
                    (int)other.Position.X,
                    (int)other.Position.Y,
                    other.Width,
                    other.Height
                );


                // 1. технический фильтр
                if (self.Id >= other.Id) continue;//to avoid dublicate collisions (A,B) vs (B,A)
                    

                // 2. геймплейный фильтр
                if (!CanCollideWith(self, other)) continue;

                // 3. геометрия
                if (!selfBounds.Intersects(otherBounds)) continue;

                var side = DetectSide(selfBounds, otherBounds);

                events.Add(new CollisionEvent
                {
                    A = self,
                    B = other,
                    Side = side
                });

                if (self.IsTrigger || other.IsTrigger) continue;

                // только реакция, без snap
                if (side == CollisionSide.Left || side == CollisionSide.Right)
                    state.Velocity.X = 0;

                if (side == CollisionSide.Bottom) {
                    state.Velocity.Y = 0;
                    state.Contacts.IsGrounded = true;
                }
                else if (side == CollisionSide.Top) {
                    state.Velocity.Y = 0;
                }
            }
        }

        public bool CanCollideWith(Entity self, Entity other)
        {
            if (self.Tag == EntityTag.Enemy && other.Tag ==  EntityTag.Enemy)
                return false;

            return true;
        }

        private void BuildContacts(ref PhysicsState state, Entity self, TileMap map)
        {
            int left = (int)(state.Position.X / map.TileSize);
            int right = (int)((state.Position.X + self.Width - 1) / map.TileSize);
            int bottom = (int)((state.Position.Y + self.Height) / map.TileSize);

            // здесь возможен оператор |= , потому что ранее мы могли приземлиться на какой то entity, но еще не приземлиться на tile
            state.Contacts.IsGrounded = map.IsSolid(left, bottom) || map.IsSolid(right, bottom);
        }

        private void Commit(Entity e, PhysicsState state)
        {
            e.Position = state.Position;
            e.Velocity = state.Velocity;
            e.Contacts = state.Contacts;
        }
    }

}