using Sprint.Interfaces;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Sprites;
using Microsoft.Xna.Framework;
using Sprint.UI.Hud;

namespace Sprint.UI;

class HUDBar : IUIElement
{
    public enum State
    {
        UNFOCUSED,
    }
    private Texture2D texture;
    private StaticSprite background;
    private Rectangle sourceRect;

    private HeartDisplay hearts = new HeartDisplay(new Vector2(505, 100), 10);

    private NumberDisplay rupeeCount;
    private NumberDisplay rupeeTens;
    private NumberDisplay rupeeOnes;

    public HUDBar(Texture2D backgroundTexture)
    {
        texture = backgroundTexture;

        sourceRect = new Rectangle(258, 19, 256, 48);
        background = new StaticSprite(texture, new Vector2(0, 0), sourceRect);

        rupeeCount = new NumberDisplay(texture, new Vector2(96 * GameServices.ScaleFactor, 8 * GameServices.ScaleFactor), -1);
        rupeeTens = new NumberDisplay(texture, new Vector2(104 * GameServices.ScaleFactor, 8 * GameServices.ScaleFactor), -1);
        rupeeOnes = new NumberDisplay(texture, new Vector2(112 * GameServices.ScaleFactor, 8 * GameServices.ScaleFactor), -1);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        background.Draw(spriteBatch, new Vector2(sourceRect.Width / 2, sourceRect.Height / 2));
        // todo use Link's hearts
        hearts.Draw(GameServices.Link.Health, GameServices.Link.MaxHealth, spriteBatch);
        rupeeCount.Draw(spriteBatch);
        rupeeTens.Draw(spriteBatch);
        rupeeOnes.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        int linkRupees = GameServices.Link.Rubies;
        int linkRupeeTens = linkRupees / 10;
        int linkRupeeOnes = linkRupees % 10;
        if (rupeeTens.Num != linkRupeeTens)
        {
            rupeeTens = new NumberDisplay(texture, new Vector2(104 * GameServices.ScaleFactor, 8 * GameServices.ScaleFactor), linkRupeeTens);
        }
        if (rupeeOnes.Num != linkRupeeOnes)
        {
            rupeeOnes = new NumberDisplay(texture, new Vector2(112 * GameServices.ScaleFactor, 8 * GameServices.ScaleFactor), linkRupeeOnes);
        }
    }
}
