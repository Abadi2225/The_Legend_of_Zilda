using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace twoD_Game
{
    public interface ISprite
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Update(GameTime gameTime);
    }
}