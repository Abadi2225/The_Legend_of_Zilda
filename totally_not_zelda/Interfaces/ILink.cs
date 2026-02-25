using Microsoft.Xna.Framework;
using Sprint.Character;

namespace Sprint.Interfaces;

public interface ILink
{
	Vector2 Position { get; }
	Directions Facing { get; }
	void StartAttack();
}