using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0;

public class Sprite1 : ISprite
{
    private readonly Texture2D _texture;
    private readonly Rectangle _source;
    private Vector2 _position;

    public Sprite1(Texture2D texture, Rectangle source, Vector2 position)
    {
        _texture = texture;
        _source = source;
        _position = position;
    }

    public void Update(GameTime gameTime)
    {
        // no movement or animation
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, _source, Color.White);
    }
}