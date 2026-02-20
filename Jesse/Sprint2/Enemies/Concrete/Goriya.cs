using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;

namespace Sprint.Enemies.Concrete
{
    public class Goriya : Enemy
    {
        private const int HEALTH = 3;
        private const int DAMAGE = 1;
        private const float STEP_SIZE = 16f;  // One tile/step
        private const float STEP_DELAY = 0.5f;  // Time between steps
        private const float MOVE_SPEED = 100f;  // Speed of actual movement
        
        private enum Direction { Up, Down, Left, Right }
        
        private Direction currentDirection;
        private Vector2 targetPosition;
        private bool isMovingToTarget;
        private float stepTimer;
        private readonly Random random;
        
        // Sprite positions for each direction
        private readonly int[] upFrames = [239];      
        private readonly int[] downFrames = [222];   
        private readonly int[] sideFrames = [256, 273];
        
        // Attacks with boomerangs
        // Drops a heart, one rupee, four bombs, or a clock
        // Moves slowly in random directions, one step at a time
        
        public Goriya(Texture2D texture, Vector2 position) : base(texture, position, HEALTH, DAMAGE)
        {
            this.texture = texture;
            int sheetY = 11;
            int spriteWidth = 15;
            int spriteHeight = 15;
            float frameTime = 0.2f;
            
            random = new Random();
            currentDirection = Direction.Down;
            targetPosition = position;
            isMovingToTarget = false;
            stepTimer = STEP_DELAY;
            
            sprite = new DirectionalAnimatedSprite(texture, position, downFrames, sheetY, 
                                        spriteWidth, spriteHeight, frameTime, true);
        }
        
       public override int Update(GameTime gameTime)
        {
            if (!isAlive)
                return base.Update(gameTime);
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (isMovingToTarget)
            {
                Vector2 currentPos = Position;
                Vector2 direction = targetPosition - currentPos;
                float distance = direction.Length();

                if (distance < MOVE_SPEED * dt)
                {
                    Position = targetPosition;
                    isMovingToTarget = false;
                    stepTimer = STEP_DELAY;
                }
                else
                {
                    direction.Normalize();
                    Position = currentPos + (direction * MOVE_SPEED * dt);
                }
            }
            else
            {
                stepTimer -= dt;
                if (stepTimer <= 0)
                {
                    TakeStep();
                }
            }
            
            return sprite.Update(gameTime);
        }
        
        private void TakeStep()
        {
            int numSteps = random.Next(1, 3);
            float totalDistance = STEP_SIZE * numSteps;
            
            currentDirection = (Direction)random.Next(4);
            
            Vector2 currentPos = sprite.Position;
            switch (currentDirection)
            {
                case Direction.Up:
                    targetPosition = new Vector2(currentPos.X, currentPos.Y - totalDistance);
                    UpdateSpriteDirection(upFrames, false);
                    break;
                case Direction.Down:
                    targetPosition = new Vector2(currentPos.X, currentPos.Y + totalDistance);
                    UpdateSpriteDirection(downFrames, false);
                    break;
                case Direction.Left:
                    targetPosition = new Vector2(currentPos.X - totalDistance, currentPos.Y);
                    UpdateSpriteDirection(sideFrames, true);
                    break;
                case Direction.Right:
                    targetPosition = new Vector2(currentPos.X + totalDistance, currentPos.Y);
                    UpdateSpriteDirection(sideFrames, false);
                    break;
            }
            
            isMovingToTarget = true;
        }
        
        private void UpdateSpriteDirection(int[] frames, bool flipHorizontal)
        {
            int sheetY = 11;
            int spriteWidth = 15;
            int spriteHeight = 15;
            float frameTime = 0.2f;
            
            Vector2 currentPos = sprite.Position;
            
            var dirSprite = sprite as DirectionalAnimatedSprite;
            if (dirSprite != null)
            {
                dirSprite.UpdateFrames(frames, flipHorizontal);
            }
            else
            {
                sprite = new DirectionalAnimatedSprite(texture, currentPos, frames, sheetY, 
                                            spriteWidth, spriteHeight, frameTime, flipHorizontal);
            }
        }
    }
}