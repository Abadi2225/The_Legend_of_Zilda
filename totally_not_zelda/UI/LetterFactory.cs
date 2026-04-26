using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint.UI
{
	internal class LetterFactory
	{
		private const int CharWidth = 8;
		private const int CharHeight = 8;

		private const int StartX = 336;
		private const int StartY = 24;
		private const int SpacingX = 16;
		private const int SpacingY = 16;

		private const string Row0 = "0123456789ABCDEF";
		private const string Row1 = "GHIJKLMNOPQRSTUV";
		private const string Row2 = "WXYZ,!'&.\"?-";

		public Rectangle GetLetter(char c)
		{
			c = char.ToUpper(c);

			int row = -1;
			int col = Row0.IndexOf(c);

			if (col >= 0)
			{
				row = 0;
			}
			else if ((col = Row1.IndexOf(c)) >= 0)
			{
				row = 1;
			}
			else if ((col = Row2.IndexOf(c)) >= 0)
			{
				row = 2;
			}
			else
			{
				return Rectangle.Empty;
			}

			return new Rectangle(
				StartX + col * SpacingX,
				StartY + row * SpacingY,
				CharWidth,
				CharHeight
			);
		}
	}
}