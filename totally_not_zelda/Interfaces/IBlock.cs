using Microsoft.Xna.Framework;

namespace Sprint.Interfaces;

public interface IBlock : IPositionedSprite
{
    bool Walkable { get; }
    Rectangle Rect { get; }
}
