using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Interfaces;
using Sprint.Sound;
using System.Collections.Generic;

namespace Sprint.UI.Text
{
	internal class TextWriter : IUIElement
	{
		private const int CharWidth = 8;
		private const int CharSpacing = 1;

		private int printedCharactersCount;
		private double timer;

		private readonly Texture2D fontsheet;
		private readonly LetterFactory letterFactory;
		private readonly string text;
		private readonly Vector2 position;
		private readonly float scale;
		private readonly bool effect;

		public bool Finished => printedCharactersCount >= text.Length;
		public TextWriter(Texture2D sheet, string text, Vector2 position, float scale, bool effect)
		{
			this.fontsheet = sheet;
			this.text = text;
			this.position = position;
			this.scale = scale;
			this.effect = effect;

			letterFactory = new LetterFactory();
			printedCharactersCount = effect ? 0 : text.Length;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 drawPos = position;

			for (int i = 0; i < printedCharactersCount; i++)
			{
				char c = text[i];

				if (c == ' ')
				{
					drawPos.X += (CharWidth + CharSpacing) * scale;
					continue;
				}

				if (c != ' ')
				{
					Rectangle charSprite = letterFactory.GetLetter(c);

					if (charSprite != Rectangle.Empty)
					{
						spriteBatch.Draw(
							fontsheet,
							drawPos,
							charSprite,
							Color.White,
							0f,
							Vector2.Zero,
							scale,
							SpriteEffects.None,
							0f
						);
					}
					drawPos.X += (CharWidth + CharSpacing) * scale;
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			if (!effect) return;

			if (printedCharactersCount >= text.Length) return;

			timer += gameTime.ElapsedGameTime.TotalSeconds;

			if (timer >= 0.1)
			{
				printedCharactersCount++;
				SoundPlayer.Play(SoundType.Text);
				timer = 0;
			}
		}
		public static TextWriter CreateGameOverText(Texture2D fontSheet)
		{
			return new TextWriter(
				fontSheet,
				"GAME OVER",
				new Vector2(260f, 300f),
				3f,
				false
			);
		}
		public static TextWriter[] CreateNPCText(Texture2D fontSheet, string[] lines, int dungeon)
		{
			float scale = 3f;
			Vector2 start = new Vector2(140f, 250f);
			float lineGap = 40f;
			float LineIndent = 220f;

			if (dungeon == 2)
			{
				scale = 2.5f;                       
				start = new Vector2(140f, 250f);  
				lineGap = 35f;                    
			}

			TextWriter[] writers = new TextWriter[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				float x = i == 1 ? LineIndent : start.X;

				writers[i] = new TextWriter(
					fontSheet,
					lines[i],
					new Vector2(x, start.Y + i * lineGap),
					scale,
					true
				);
			}
			return writers;
		}
	}
}