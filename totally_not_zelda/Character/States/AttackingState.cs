using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class AttackingState : LinkState
{
    public override void OnEnter(Link link)
    {
        link.Move = Vector2.Zero;

        Attacking attackSprite = link.Direction switch
        {
            Directions.Up => link.AttackUp,
            Directions.Down => link.AttackDown,
            Directions.Left => link.AttackLeft,
            Directions.Right => link.AttackRight,
            _ => link.AttackDown,
        };

        attackSprite.Reset();
        link.Sprite = attackSprite;
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        link.Sprite.Update(gameTime);
    }
}
