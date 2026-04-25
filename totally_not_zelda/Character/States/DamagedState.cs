using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class DamagedState : LinkState
{
    private const double DAMAGED_DURATION = 3;
    private const double BLINK_INTERVAL = 0.10;
    private const float SPEED = 80f;

    private double timer;
    private Vector2 moveVector;

    public bool IsVisible { get; private set; }

    public void SetMove(Directions dir, Link link)
    {
        link.Direction = dir;
        moveVector = dir switch
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

    public void StopMove(Link link)
    {
        moveVector = Vector2.Zero;
        link.Sprite = link.Direction switch
        {
            Directions.Up => link.IdleUp,
            Directions.Down => link.IdleDown,
            Directions.Left => link.IdleLeft,
            Directions.Right => link.IdleRight,
            _ => link.IdleDown,
        };
    }

    public override void OnEnter(Link link)
    {
        timer = 0;
        moveVector = Vector2.Zero;
        IsVisible = true;
        link.Move = Vector2.Zero;
        link.Sprite = link.Direction switch
        {
            Directions.Up => link.IdleUp,
            Directions.Down => link.IdleDown,
            Directions.Left => link.IdleLeft,
            Directions.Right => link.IdleRight,
            _ => link.IdleDown,
        };
    }

    public override void OnExit(Link link)
    {
        IsVisible = true;
        link.Move = Vector2.Zero;
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        timer += dt;

        IsVisible = (int)(timer / BLINK_INTERVAL) % 2 == 0;

        if (timer >= DAMAGED_DURATION)
        {
            sm.TransitionToIdle();
            return;
        }

        if (moveVector != Vector2.Zero)
            link.Position += moveVector * SPEED * dt;

        link.Sprite.Update(gameTime);
    }
}
