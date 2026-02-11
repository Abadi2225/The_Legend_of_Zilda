using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace sprint0;

public class MouseController : IController
{
    private readonly Game1 _game;
    private readonly ISprite _s1;
    private readonly ISprite _s2;
    private readonly ISprite _s3;
    private readonly ISprite _s4;

    private MouseState _prev;

    public MouseController(Game1 game, ISprite s1, ISprite s2, ISprite s3, ISprite s4)
    {
        _game = game;
        _s1 = s1;
        _s2 = s2;
        _s3 = s3;
        _s4 = s4;
    }

    public void Update()
    {
        MouseState cur = Mouse.GetState();

        bool leftClick = cur.LeftButton == ButtonState.Pressed && _prev.LeftButton == ButtonState.Released;
        bool rightClick = cur.RightButton == ButtonState.Pressed && _prev.RightButton == ButtonState.Released;

        if (rightClick)
        {
            _game.Exit();
            _prev = cur;
            return;
        }

        if (leftClick)
        {
            int w = _game.GraphicsDevice.Viewport.Width;
            int h = _game.GraphicsDevice.Viewport.Height;

            bool left = cur.X < w / 2;
            bool top = cur.Y < h / 2;

            if (left && top) _game.SetCurrentSprite(_s1);
            else if (!left && top) _game.SetCurrentSprite(_s2);
            else if (left && !top) _game.SetCurrentSprite(_s3);
            else _game.SetCurrentSprite(_s4);
        }

        _prev = cur;
    }
}
