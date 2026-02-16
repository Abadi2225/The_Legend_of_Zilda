using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint.Interfaces;
using Sprint.Controllers;
using Sprint.Sprites;
using Sprint.Enemies.Gel;

namespace Sprint;

public class Game1 : Game
{
    private Texture2D credits;
    private Texture2D linkSheet;
    private Texture2D enemiesSheet;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private IController keyboard;

    private IController mouse;

    private GameState currState;
    private ISprite currSprite;

    private ISprite staticSprite;
    private ISprite animatedSprite;
    private ISprite movingSprite;
    private ISprite movingAnimatedSprite;
    private ISprite gelSprite;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        //Switch this to Start Screen after we implement it
        currState = GameState.Running;
    }

    protected override void Initialize()
    {
        keyboard = new KeyboardController(this);
        mouse = new MouseController(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

        base.Initialize();
    }


    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        linkSheet = Content.Load<Texture2D>("images/Link");

        Vector2 center = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

        //Left these as examples for how to load sprites with the current parameters

        staticSprite = new StaticSprite(linkSheet, center, new Rectangle(0, 11, 16, 16));

        animatedSprite = new AnimatedSprite(linkSheet, center, new int[] { 0, 17 }, 11, 16, 16, 0.2f);

        movingSprite = new MovingSprite(linkSheet, center, new int[] { 68, 85 }, 11, 16, 16, 0.2f);

        movingAnimatedSprite = new MovingAnimatedSprite(linkSheet, center, new int[] { 34, 51 }, 11, 16, 16, 0.2f);
        
        //Enemies
        
        gelSprite = new Gel(enemiesSheet, center);

        SetState(currState);
    }

    //TODO write some functionality to load and unload the enemies as necessary in different positions
    protected override void Update(GameTime gameTime)
    {
        keyboard.Update();
        mouse.Update();

        if (currSprite != null)
        {
            currSprite.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        //float creditsScale = 0.3f;
        //float creditsX = (Window.ClientBounds.Width - credits.Width * creditsScale) / 2;
        //float creditsY = Window.ClientBounds.Height - credits.Height * creditsScale - 10;

        spriteBatch.Draw
    
        //spriteBatch.Draw(credits, 
        //new Vector2(creditsX, creditsY), 
       // null, 
        //Color.White, 
        //0f, 
       // Vector2.Zero, 
        //creditsScale, 
        //SpriteEffects.None, 
        //0f);

        if (currSprite != null)
        {
            currSprite.Draw(spriteBatch, currSprite.Position);
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }


    //Used to be animation state for Sprint0, switched to Start Screen/Pause/Running/GameOver
    public void SetState(GameState newState)
    {
        currState = newState;

        switch (currState)
            {
                case GameState.StartScreen:
                    //currSprite = staticSprite;
                    break;

                case GameState.Running:
                    //currSprite = animatedSprite;
                    break;

                case GameState.Pause:
                    //currSprite = movingSprite;
                    break;

                case GameState.GameOver:
                    //currSprite = movingAnimatedSprite;
                    break;
            }        
    }

    public GameState GetCurrentState()
    {
        return currState;
    }
}
