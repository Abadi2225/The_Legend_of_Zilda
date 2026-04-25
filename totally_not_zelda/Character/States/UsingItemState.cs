using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class UsingItemState : LinkState
{
    public override void OnEnter(Link link)
    {
        link.Move = Vector2.Zero;

        UseItem useSprite = link.Direction switch
        {
            Directions.Up => link.UseItemUp,
            Directions.Down => link.UseItemDown,
            Directions.Left => link.UseItemLeft,
            Directions.Right => link.UseItemRight,
            _ => link.UseItemDown,
        };

        useSprite.Reset();
        link.Sprite = useSprite;
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        link.Sprite.Update(gameTime);
    }
}
