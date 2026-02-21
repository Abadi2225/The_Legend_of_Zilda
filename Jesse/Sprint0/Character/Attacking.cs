using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Interfaces;

namespace Sprint.Character
{
    internal class Attacking : ISprite
    {
        private readonly Texture2D texture;
        private readonly SpriteEffects effect;
        private readonly Rectangle[] frames;
        private int currentFrame;
        private double timer;
        private readonly double secondsPerFrame;

        // Total time to stay in attack state (can be longer than frames*spf if you want)
        private readonly double totalAttackSeconds;
        private double totalTimer;

        public Vector2 Position { get; set; } // unused; required by ISprite

        // onFinished is called once the attack completes (Link will swap back to Idle)
        private readonly System.Action onFinished;
        private bool finished;

        public Attacking(
            Texture2D texture,
            SpriteEffects effect,
            Rectangle[] frames,
            double secondsPerFrame,
            double totalAttackSeconds,
            System.Action onFinished)
        {
            this.texture = texture;
            this.effect = effect;
            this.frames = frames;
            this.secondsPerFrame = secondsPerFrame;
            this.totalAttackSeconds = totalAttackSeconds;
            this.onFinished = onFinished;

            currentFrame = 0;
            timer = 0;
            totalTimer = 0;
            finished = false;
        }

        public int Update(GameTime gameTime)
        {
            if (finished) return currentFrame;

            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            timer += dt;
            totalTimer += dt;

            if (timer >= secondsPerFrame)
            {
                currentFrame = (currentFrame + 1);
                if (currentFrame >= frames.Length) currentFrame = frames.Length - 1; // hold last frame
                timer = 0;
            }

            if (totalTimer >= totalAttackSeconds)
            {
                finished = true;
                onFinished?.Invoke();
            }

            return currentFrame;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(
                texture,
                location,
                frames[currentFrame],
                Color.White,
                0f,
                Vector2.Zero,
                2f,
                effect,
                0f
            );
        }
    }
}
