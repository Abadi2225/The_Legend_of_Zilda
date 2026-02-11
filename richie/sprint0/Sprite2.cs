using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0;

public class Sprite2 : ISprite
{
    private readonly Texture2D _texture;
    private readonly List<Rectangle> _frames;
    private readonly float _secondsPerFrame;

    private Vector2 _position;
    private int _frameIndex;
    private float _timer;

    public Sprite2(Texture2D texture, List<Rectangle> frames, Vector2 position, float secondsPerFrame = 0.15f)
    {
        _texture = texture;
        _frames = frames;
        _position = position;
        _secondsPerFrame = secondsPerFrame;
        _frameIndex = 0;
        _timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
