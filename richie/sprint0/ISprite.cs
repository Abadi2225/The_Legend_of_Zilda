using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0;

public interface ISprite
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}