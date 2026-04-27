using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Interfaces;

namespace Sprint.UI.Text
{
	internal class TextWriterSequence : IUIElement
	{
		private readonly TextWriter[] writers;
		private int currentIndex;

		public TextWriterSequence(TextWriter[] writers)
		{
			this.writers = writers;
			currentIndex = 0;
		}

		public void Update(GameTime gameTime)
		{
			if (currentIndex >= writers.Length) return;

			writers[currentIndex].Update(gameTime);

			if (writers[currentIndex].Finished)
				currentIndex++;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var writer in writers)
				writer.Draw(spriteBatch);
		}
	}
}