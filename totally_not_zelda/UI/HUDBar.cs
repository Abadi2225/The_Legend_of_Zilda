using Sprint.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Sprites;
using Microsoft.Xna.Framework;

namespace Sprint.UI;

class HUDBar : IUIElement
{
    private StaticSprite background;
    private Rectangle sourceRect;
    
    public HUDBar(Texture2D backgroundTexture)
    {
        sourceRect = new Rectangle(258, 11, 256, 56);
        background = new StaticSprite(backgroundTexture, new Vector2(0, 0), sourceRect);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        background.Draw(spriteBatch, new Vector2(sourceRect.Width / 2, sourceRect.Height / 2));
    }

    public void Update(GameTime gameTime)
    {
    }
}