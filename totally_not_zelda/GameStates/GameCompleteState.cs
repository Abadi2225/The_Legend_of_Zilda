using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint.Commands;
using Sprint.Interfaces;
using Sprint.Sound;
using Sprint.UI.Text;

namespace Sprint.GameStates;

internal class GameCompleteState : IGameState
{
    private Texture2D fontSheet;
    private Texture2D pixel;
    private TextWriter gameOverText;
    private TextWriter pressRText;

    public void Enter()
    {
        MusicPlayer.Mute();
    }

    public void Exit() { }

    public void LoadContent()
    {
        fontSheet = GameServices.Content.Load<Texture2D>("images/Fonts");

        pixel = new Texture2D(GameServices.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        gameOverText = new TextWriter(
            fontSheet,
            "GAME OVER",
            new Vector2(260f, 300f),
            3f,
            false
        );

        pressRText = new TextWriter(
            fontSheet,
            "PRESS R TO RESET",
            new Vector2(240f, 370f),
            2f,
            false
        );
    }

    public void Update(GameTime gameTime)
    {
        if (GameServices.KeyInput.IsKeyPressed(Keys.R))
            new RestartGameCommand().Execute();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            pixel,
            new Rectangle(0, 0, GameServices.GameWidth, GameServices.GameHeight),
            Color.Black
        );
        gameOverText.Draw(spriteBatch);
        pressRText.Draw(spriteBatch);
    }
}
