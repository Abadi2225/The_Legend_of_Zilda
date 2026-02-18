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

		private ISprite IdleUp;
		private ISprite IdleDown;
		private ISprite IdleLeft;
		private ISprite IdleRight;

		private ISprite sprite;

		private ISprite WalkUp;
		private ISprite WalkDown;
		private ISprite WalkLeft;
		private ISprite WalkRight;

		private int elapsedTime;
		private int frameTime;
		private float speed = 80f;
		Vector2 move = Vector2.Zero;
		private Rectangle bounds;
		

		private enum Directions
		{
			Left,
			Right,
			Up,
			Down,
		}

		private Directions direction = Directions.Down;

		public Link(Texture2D texture, Vector2 position)
		{
			this.position = position;
			this.texture = texture;

			IdleDown = LinkSprites.IdleDown(texture);
			IdleUp = LinkSprites.IdleUp(texture);
			IdleLeft = LinkSprites.IdleLeft(texture);
			IdleRight = LinkSprites.IdleRight(texture);

			WalkDown = LinkSprites.WalkingDown(texture);
			WalkUp = LinkSprites.WalkingUp(texture);
			WalkLeft = LinkSprites.WalkingLeft(texture);
			WalkRight = LinkSprites.WalkingRight(texture);

			sprite = IdleDown;
		}

		public void Update(GameTime gameTime)
		{
			move = Vector2.Zero;
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			KeyboardState command = Keyboard.GetState();

			if(command.IsKeyDown(Keys.W) || command.IsKeyDown(Keys.Up))
			{
				direction = Directions.Up;
				sprite = WalkUp;
				move.Y = -1;

			}
			else if(command.IsKeyDown(Keys.S) || command.IsKeyDown(Keys.Down))
			{

				direction = Directions.Down;
				sprite = WalkDown;
				move.Y = 1;
			}
			else if (command.IsKeyDown(Keys.A) || command.IsKeyDown(Keys.Left))
			{
				direction = Directions.Left;
				sprite = WalkLeft;
				move.X = -1;
			}
			else if (command.IsKeyDown(Keys.D) || command.IsKeyDown(Keys.Right))
			{
				direction = Directions.Right;
				sprite = WalkRight;
				move.X = 1;
			}

			if (move == Vector2.Zero)
			{
				switch (direction)
				{
					case Directions.Up:
						sprite = IdleUp;
						break;
					case Directions.Down:
						sprite = IdleDown;
						break;
					case Directions.Left:
						sprite = IdleLeft;
						break;
					case Directions.Right:
						sprite = IdleRight;
						break;
				}
			}

			Vector2 newPos = position + move * speed * dt;

			sprite.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			sprite.Draw(spriteBatch, position);
		}
	}
}
