using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;

namespace Sprint.Enemies.Gel
{
    public class Gel : Enemy
    {
        private const int GEL_HEALTH = 1;
        private const int GEL_DAMAGE = 1;
        
        // Bouncing movement
        private Vector2 velocity;
        private float bounceTimer;
        private bool isOnGround;
        private const float BOUNCE_SPEED = 80f;
        private const float BOUNCE_INTERVAL = 0.6f;
        private const float AIR_TIME = 0.4f;
        private readonly Random random;
        
        public Gel(Texture2D texture, Vector2 position) : base(texture, position, GEL_HEALTH, GEL_DAMAGE)
        {
            int[] frameXPositions = new int[] { 1, 10 };
            int frameY = 15;
            int spriteWidth = 7;
            int spriteHeight = 8;
            float frameTime = 0.2f;
            
            sprite = new AnimatedSprite(texture, position, frameXPositions, frameY, 
                                        spriteWidth, spriteHeight, frameTime);
            
            random = new Random();
            isOnGround = true;
            bounceTimer = BOUNCE_INTERVAL;
            velocity = Vector2.Zero;
        }
        
        private Vector2 GetRandomBounceDirection()
        {
            float angle = (float)(random.NextDouble() * MathHelper.TwoPi);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * BOUNCE_SPEED;
        }
        
        public override int Update(GameTime gameTime)
        {
            if (!isAlive)
                return base.Update(gameTime);
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (isOnGround)
            {
                bounceTimer -= deltaTime;
                if (bounceTimer <= 0)
                {
                    isOnGround = false;
                    velocity = GetRandomBounceDirection();
                    bounceTimer = AIR_TIME;
                }
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
            
            return base.Update(gameTime);
        }
    }
}