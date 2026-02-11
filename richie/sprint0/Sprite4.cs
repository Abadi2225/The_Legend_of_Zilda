using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0;

public class Sprite4 : ISprite
{
    private readonly Texture2D _texture;
    private readonly List<Rectangle> _frames;
    private readonly float _secondsPerFrame;

    private Vector2 _position;
    private int _frameIndex;
    private float _timer;

    private float _speed = 120f;
    private int _direction = 1; // 1 is right and -1 is left

    private float _minX = 0f;
    private readonly float _maxX;

    public Sprite4(
        Texture2D texture,
        List<Rectangle> frames,
        Vector2 startPos,
        float maxX,
        float secondsPerFrame = 0.12f
    )
    {
        _texture = texture;
        _frames = frames;
        _position = startPos;
        _maxX = maxX;
        _secondsPerFrame = secondsPerFrame;
        _frameIndex = 0;
        _timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _position.X += _direction * _speed * dt;

        if (_position.X < _minX) _direction = 1;
        if (_position.X > _maxX) _direction = -1;

        _timer += dt;
        if (_timer >= _secondsPerFrame)
        {
            _timer -= _secondsPerFrame;
            _frameIndex = (_frameIndex + 1) % _frames.Count;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, _frames[_frameIndex], Color.White);
    }
}
