using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;

namespace Sprint.Enemies.Goriya
{
    public class Goriya : Enemy
    {
        private const int HEALTH = 1;
        private const int DAMAGE = 1;
        
        // Attacks with boomerangs
        // Drops a heart, one rupee, four bombs, or a clock
        
        // TODO point to right texture on sheet
        public Goriya(Texture2D texture, Vector2 position) : base(texture, position, HEALTH, DAMAGE)
        {
            int[] sheetXPositions = new int[] { 1, 10 };
            int sheetY = 15;
            int spriteWidth = 7;
            int spriteHeight = 8;
            float frameTime = 0.2f;
            
            sprite = new AnimatedSprite(texture, position, sheetXPositions, sheetY, 
                                        spriteWidth, spriteHeight, frameTime);
        }
            
        public override int Update(GameTime gameTime)
        {
            if (!isAlive)
                return base.Update(gameTime);
        }   
            
    }
}