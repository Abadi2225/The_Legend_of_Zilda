using Microsoft.Xna.Framework.Graphics;
using Sprint.Interfaces;
using Sprint.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Sprint.Factories
{
	internal static class LinkSprites
	{
		public static ISprite IdleDown(Texture2D texture) => new Idle(texture, new Rectangle(1, 11, 16, 16), SpriteEffects.None);
		public static ISprite IdleUp(Texture2D texture) => new Idle(texture, new Rectangle(69, 11, 16, 16), SpriteEffects.None);
		public static ISprite IdleLeft(Texture2D texture) => new Idle(texture, new Rectangle(35, 11, 16, 16), SpriteEffects.FlipHorizontally);
		public static ISprite IdleRight(Texture2D texture) => new Idle(texture, new Rectangle(35, 11, 16, 16), SpriteEffects.None);
		public static ISprite WalkingDown(Texture2D texture)
		{
			Rectangle[] frames = new Rectangle[]
			{
			new Rectangle(1, 11, 16, 16),
			new Rectangle(18, 11, 16, 16)
			};

			return new Walking(texture, SpriteEffects.None, frames, 0.15);
		}
		public static ISprite WalkingUp(Texture2D texture)
		{
			Rectangle[] frames = new Rectangle[]
			{
			new Rectangle(69, 11, 16, 16),
			new Rectangle(86, 11, 16, 16)
			};

			return new Walking(texture, SpriteEffects.None, frames, 0.15);
		}
		public static ISprite WalkingLeft(Texture2D texture)
		{
			Rectangle[] frames = new Rectangle[]
			{
			new Rectangle(35, 11, 16, 16),
			new Rectangle(52, 11, 16, 16)
			};

			return new Walking(texture, SpriteEffects.FlipHorizontally, frames, 0.15);
		}
		public static ISprite WalkingRight(Texture2D texture)
		{
			Rectangle[] frames = new Rectangle[]
			{
			new Rectangle(35, 11, 16, 16),
			new Rectangle(52, 11, 16, 16)
			};

			return new Walking(texture, SpriteEffects.None, frames, 0.15);
		}
	}
}
