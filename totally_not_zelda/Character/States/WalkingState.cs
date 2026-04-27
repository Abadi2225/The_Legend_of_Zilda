using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class WalkingState : LinkState
{
    private const float SPEED = 80f;
    private const float PUSHING_SPEED = 40f;
    private const float PUSHING_DURATION = 0.5f;

    public bool IsPushing { get; private set; }
    private double pushingTimer;
	private Vector2 move = Vector2.Zero;

	public void StartPushing()
    {
        IsPushing = true;
        pushingTimer = 0;
    }

    public void SetDirection(Directions dir, Link link)
    {
        link.Direction = dir;
        move = dir switch
        {
            Directions.Up => new Vector2(0, -1 * GameServices.ScaleFactor),
            Directions.Down => new Vector2(0, 1 * GameServices.ScaleFactor),
            Directions.Left => new Vector2(-1 * GameServices.ScaleFactor, 0),
            Directions.Right => new Vector2(1 * GameServices.ScaleFactor, 0),
            _ => Vector2.Zero,
        };

        link.Sprite = dir switch
        {
            Directions.Up => link.WalkUp,
            Directions.Down => link.WalkDown,
            Directions.Left => link.WalkLeft,
            Directions.Right => link.WalkRight,
            _ => link.WalkDown,
        };
    }

    public override void OnEnter(Link link)
    {
        IsPushing = false;
        pushingTimer = 0;
    }

    public override void OnExit(Link link)
    {
        IsPushing = false;
        move = Vector2.Zero;
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (IsPushing)
        {
            pushingTimer += dt;
            if (pushingTimer >= PUSHING_DURATION)
                IsPushing = false;
        }

        float speed = IsPushing ? PUSHING_SPEED : SPEED;
        link.Position += move * speed * dt;
        link.Sprite.Update(gameTime);
    }
}
