using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;

namespace Sprint.Enemies.Keese
{
    public class Keese : Enemy
    {
        private const int HEALTH = 1;
        private const int DAMAGE = 1;
        
        // Rests against walls first before taking flight
        // Moves erratically in random directions, stopping sometimes to rest
        // Boomerang kills them instead of stunning
        // Never drop any items
        
        // TODO point to right texture on sheet
        public Keese(Texture2D texture, Vector2 position) : base(texture, position, HEALTH, DAMAGE)
        {
            int[] frameXPositions = new int[] { 1, 10 };
            int frameY = 15;
            int spriteWidth = 7;
            int spriteHeight = 8;
            float frameTime = 0.2f;
            
            sprite = new AnimatedSprite(texture, position, frameXPositions, frameY, 
                                        spriteWidth, spriteHeight, frameTime);
        }
            
        public override int Update(GameTime gameTime)
        {
            if (!isAlive)
                return base.Update(gameTime);
        }   
            
    }
}