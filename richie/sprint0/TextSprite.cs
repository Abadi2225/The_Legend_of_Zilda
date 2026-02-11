using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0;

public class TextSprite : ISprite
{
    private readonly SpriteFont _font;
    private readonly string _text;
    private readonly Vector2 _position;

    public TextSprite(SpriteFont font, string text, Vector2 position)
    {
        _font = font;
        _text = text;
        _position = position;
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, _text, _position, Color.Black);
    }
}
