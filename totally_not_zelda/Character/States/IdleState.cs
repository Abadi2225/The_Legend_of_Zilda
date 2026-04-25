using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class IdleState : LinkState
{
    public override void OnEnter(Link link)
    {
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

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        link.Sprite.Update(gameTime);
    }
}
