using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;

namespace Sprint.Enemies.Concrete
{
    public class Gel : Enemy
    {
<<<<<<< HEAD
        private const int GEL_HEALTH = 1;
        private const int GEL_DAMAGE = 1;
        
        private Vector2 velocity;
        private float TurnTimer;
        private const float TURN_SPEED = 30f;
        private const float TURN_INTERVAL = 1f;
        private const float MOVE_SPEED = 0.7f;
=======
        private const int HEALTH = 1;
        private const int DAMAGE = 1;
        private const float BOUNCE_SPEED = 30f;
        private const float BOUNCE_INTERVAL = 1f;
        private const float AIR_TIME = 0.7f;

>>>>>>> 500ecab1ae7c71915114ee4282d883ab7f0986f1
        private readonly Random random;
        private Vector2 velocity;
        private float bounceTimer;
        private bool isOnGround;

        public Gel(Texture2D texture, Vector2 position) : base(texture, position, HEALTH, DAMAGE)
        {
            int[] frameXPositions = [1, 10];
            int frameY = 15;
            int spriteWidth = 7;
            int spriteHeight = 8;
            float frameTime = 0.2f;

            sprite = new AnimatedSprite(texture, position, frameXPositions, frameY,
                                        spriteWidth, spriteHeight, frameTime);

            random = new Random();
            TurnTimer = TURN_INTERVAL;
            velocity = Vector2.Zero;
        }
<<<<<<< HEAD
        
        private Vector2 GetRandomCardinalDirection()
        {
            int direction = random.Next(4);
            return direction switch
            {
                0 => new Vector2(0, -TURN_SPEED),// Up
                1 => new Vector2(0, TURN_SPEED),// Down
                2 => new Vector2(-TURN_SPEED, 0),// Left
                3 => new Vector2(TURN_SPEED, 0),// Right
                _ => Vector2.Zero,
            };
=======
>>>>>>> 500ecab1ae7c71915114ee4282d883ab7f0986f1

        public override int Update(GameTime gameTime)
        {
            if (!isAlive)
                return base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
<<<<<<< HEAD
            
            
                TurnTimer -= deltaTime;
                if (TurnTimer <= 0)
=======

            if (isOnGround)
            {
                bounceTimer -= deltaTime;
                if (bounceTimer <= 0)
>>>>>>> 500ecab1ae7c71915114ee4282d883ab7f0986f1
                {
                    velocity = GetRandomCardinalDirection();
                    TurnTimer = MOVE_SPEED;
                }
<<<<<<< HEAD
            
            
            
=======
            }
            else
            {
                bounceTimer -= deltaTime;
                Position += velocity * deltaTime;

                if (bounceTimer <= 0)
                {
                    isOnGround = true;
                    velocity = Vector2.Zero;
                    bounceTimer = BOUNCE_INTERVAL;
                }
            }

>>>>>>> 500ecab1ae7c71915114ee4282d883ab7f0986f1
            return base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
            isOnGround = true;
            bounceTimer = BOUNCE_INTERVAL;
            velocity = Vector2.Zero;
        }

        private Vector2 GetRandomBounceDirection()
        {
            return random.Next(4) switch
            {
                0 => new Vector2(0, -BOUNCE_SPEED),   // Up
                1 => new Vector2(0, BOUNCE_SPEED),    // Down
                2 => new Vector2(-BOUNCE_SPEED, 0),   // Left
                3 => new Vector2(BOUNCE_SPEED, 0),    // Right
                _ => Vector2.Zero,
            };
        }
    }
}