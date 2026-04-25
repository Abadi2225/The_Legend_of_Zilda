using Microsoft.Xna.Framework;
using Sprint.Character;

namespace Sprint.Interfaces;

internal interface ILinkState
{
    void OnEnter(Link link);
    void OnExit(Link link);
    void Update(Link link, LinkStateMachine sm, GameTime gameTime);
}
