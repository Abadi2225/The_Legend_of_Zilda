using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _marioSheet;
    private ISprite _currentSprite;
    private ISprite _sprite1;
    private ISprite _sprite2;
    private ISprite _sprite3;
    private ISprite _sprite4;
    private IController _keyboard;
    private IController _mouse;

    private ISprite _textSprite;
    private SpriteFont _font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        float maxY = GraphicsDevice.Viewport.Height - 24;
        float maxX = GraphicsDevice.Viewport.Width - 24;

        _marioSheet = Content.Load<Texture2D>("sprites/mario");
        _font = Content.Load<SpriteFont>("fonts/CreditsFont");

        _textSprite = new TextSprite(
            _font,
            "Credits\nProgram Made By: Richie Cooper\nSprites from: https://www.mariouniverse.com/sprites-ds-nsmb/",
            new Vector2(10, 390)
        );

        _sprite1 = new Sprite1(
            _marioSheet,
            new Rectangle(52, 0, 22, 24),
            new Vector2(400, 220)
        );

        var frames2 = new List<Rectangle>
        {
            new Rectangle(52, 0, 22, 24),
            new Rectangle(72, 0, 22, 24),
            new Rectangle(94, 0, 20, 24),
            new Rectangle(113, 0, 22, 24),

            new Rectangle(133, 0, 18, 24),
            new Rectangle(113, 0, 22, 24),
            new Rectangle(94, 0, 20, 24),
            new Rectangle(72, 0, 22, 24),
        };

        _sprite2 = new Sprite2(_marioSheet, frames2, new Vector2(400, 220), 0.12f);

        _sprite3 = new Sprite3(
        _marioSheet,
        new Rectangle(52, 0, 22, 24),
        new Vector2(400, 220),
        maxY
        );

        var frames4 = new List<Rectangle>
        {
            new Rectangle(36, 121, 18, 24),
            new Rectangle(56, 121, 18, 24),
            new Rectangle(76, 121, 18, 24),
            new Rectangle(96, 121, 18, 24),
            new Rectangle(116, 121, 16, 24),
            new Rectangle(134, 121, 18, 24),
        };

        _sprite4 = new Sprite4(_marioSheet, frames4, new Vector2(400, 220), maxX, 0.12f);

        _mouse = new MouseController(this, _sprite1, _sprite2, _sprite3, _sprite4);

        _currentSprite = _sprite1;

        var kb = new KeyboardController();
        kb.Bind(Keys.D0, new QuitCommand(this));
        kb.Bind(Keys.NumPad0, new QuitCommand(this));
        kb.Bind(Keys.D1, new SetSpriteCommand(this, _sprite1));
        kb.Bind(Keys.NumPad1, new SetSpriteCommand(this, _sprite1));
        kb.Bind(Keys.D2, new SetSpriteCommand(this, _sprite2));
        kb.Bind(Keys.NumPad2, new SetSpriteCommand(this, _sprite2));
        kb.Bind(Keys.D3, new SetSpriteCommand(this, _sprite3));
        kb.Bind(Keys.NumPad3, new SetSpriteCommand(this, _sprite3));
        kb.Bind(Keys.D4, new SetSpriteCommand(this, _sprite4));
        kb.Bind(Keys.NumPad4, new SetSpriteCommand(this, _sprite4));
        _keyboard = kb;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _keyboard.Update();
        _mouse.Update();
        _currentSprite?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        if (_spriteBatch == null) return;

        _spriteBatch.Begin();

        _currentSprite?.Draw(_spriteBatch);
        _textSprite?.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void SetCurrentSprite(ISprite sprite)
    {
        _currentSprite = sprite;
    }
}
