using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Concrete;
using Sprint.Interfaces;

namespace Sprint.Enemies
{
    public enum ProjectileType
    {
 
    }
    
    public class EnemyProjectileFactory(Texture2D enemySpriteSheet)
    {
        private readonly Texture2D enemySpriteSheet = enemySpriteSheet;

        public void LoadAllTextures(ContentManager content)
        {
            
        }

        // Can change vector2 position to something else (e.g. x/y pos) in the future
        // Switch this to projectiles for the future
       // public IEnemy CreateProjectile(EnemyType type, Vector2 position)
       // {
       //     switch (type)
       //     {
                
       //     }
      //  }   
    }
}
