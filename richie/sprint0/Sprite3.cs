using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0;

public class Sprite3 : ISprite
{
    private readonly Texture2D _texture;
    private readonly Rectangle _source;

    private Vector2 _position;
    private float _speed = 100f;
    private int _direction = 1; // 1 is down and -1 is up

    private readonly float _maxY;

    public Sprite3(Texture2D texture, Rectangle source, Vector2 startPos, float maxY)
    {
        _texture = texture;
        _source = source;
        _position = startPos;
        _maxY = maxY;
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _position.Y += _direction * _speed * dt;

        if (_position.Y < 0)
            _direction = 1;

        if (_position.Y > _maxY) 
            _direction = -1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, _source, Color.White);
    }
}
