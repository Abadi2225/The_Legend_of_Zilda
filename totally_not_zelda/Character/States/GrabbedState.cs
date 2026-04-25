using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class GrabbedState : LinkState
{
    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        link.Sprite.Update(gameTime);
    }
}
