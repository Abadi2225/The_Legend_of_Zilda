using Microsoft.Xna.Framework;
using Sprint.Interfaces;

namespace Sprint.Character.States;

internal abstract class LinkState : ILinkState
{
    public virtual void OnEnter(Link link) { }
    public virtual void OnExit(Link link) { }
    public abstract void Update(Link link, LinkStateMachine sm, GameTime gameTime);
}
