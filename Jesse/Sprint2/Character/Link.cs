using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint.Factories;
using Sprint.Interfaces;

namespace Sprint.Character
{
	internal class Link 
	{
		private Vector2 position;
		private Texture2D texture;
		private ISprite sprite;
		private const float MOVE_SPEED = 150f;

		public Link(Texture2D texture, Vector2 position)
		{
			this.position = position;
			this.texture = texture;

			sprite = LinkSprites.IdleDown(texture);
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState command = Keyboard.GetState();
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if(command.IsKeyDown(Keys.W))
			{
				sprite = LinkSprites.IdleUp(texture);
				position.Y -= MOVE_SPEED * dt;
			}
			else if(command.IsKeyDown(Keys.S))
			{
				sprite = LinkSprites.IdleDown(texture);
				position.Y += MOVE_SPEED * dt;
			}
			else if (command.IsKeyDown(Keys.A))
			{
				sprite = LinkSprites.IdleLeft(texture);
				position.X -= MOVE_SPEED * dt;
			}
			else if (command.IsKeyDown(Keys.D))
			{
				sprite = LinkSprites.IdleRight(texture);
				position.X += MOVE_SPEED * dt;
			}

			sprite.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			sprite.Draw(spriteBatch, position);
		}
	}
}
