using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies.Base;
using Sprint.Sprites;
using Sprint.Item;
using System.Collections.Generic;

namespace Sprint.Enemies.Concrete
{
    public class Goriya : Enemy
    {
        // Extract Class: Direction is promoted from a bare enum to a struct that owns
        // its own behavior. This eliminates three switch statements (Switch Statement
        // Duplication) by giving each direction knowledge of its own vector, throw origin
        // offset, and sprite frames. Adding a new direction no longer requires Shotgun
        // Surgery across UpdateSprite, ThrowBoomerang, and GetThrowOrigin.
        private readonly struct GoriyaDirection
        {
            public readonly Vector2 UnitVector;
            public readonly int[] WalkFrames;
            public readonly int[] ThrowFrames;
            public readonly bool FlipSprite;

            private GoriyaDirection(Vector2 unitVector, int[] walkFrames, int[] throwFrames, bool flipSprite)
            {
                UnitVector = unitVector;
                WalkFrames = walkFrames;
                ThrowFrames = throwFrames;
                FlipSprite = flipSprite;
            }

            public static readonly GoriyaDirection Up = new(new Vector2( 0, -1), new[] { 239 }, new[] { 239 }, false);
            public static readonly GoriyaDirection Down = new(new Vector2( 0,  1), new[] { 222 }, new[] { 222 }, false);
            public static readonly GoriyaDirection Left = new(new Vector2(-1,  0), new[] { 256, 273 }, new[] { 273 }, true);
            public static readonly GoriyaDirection Right = new(new Vector2( 1,  0), new[] { 256, 273 }, new[] { 273 }, false);

            // Move Method: throw origin calculation moved here from Goriya.GetThrowOrigin,
            // resolving Feature Envy — this method was reaching into Goriya's position and
            // scale math to produce a direction-dependent result. It belongs here.
            public Vector2 GetThrowOrigin(Vector2 position)
            {
                float scale = GameServices.ScaleFactor;
                int spriteSize = (int)(16 * scale);
                float cx = position.X + spriteSize / 2f;
                float cy = position.Y + spriteSize / 2f;

                if (UnitVector.Y < 0) return new Vector2(cx, position.Y);
                if (UnitVector.Y > 0) return new Vector2(cx, position.Y + spriteSize);
                if (UnitVector.X < 0) return new Vector2(position.X, cy);
                return new Vector2(position.X + spriteSize, cy);
            }
        }

        // Extract Class: NavigationContext groups solidBlocks and innerBounds, which
        // are a Data Clump: they are always constructed together, stored together,
        // and passed together to ChooseValidStep. Wrapping them makes the dependency
        // explicit and reduces the constructor parameter count (Long Parameters List).
        private readonly struct NavigationContext
        {
            public readonly List<Sprint.Block.Block> SolidBlocks;
            public readonly Rectangle InnerBounds;

            public NavigationContext(List<Sprint.Block.Block> solidBlocks, Rectangle innerBounds)
            {
                SolidBlocks  = solidBlocks;
                InnerBounds  = innerBounds;
            }
        }

        private enum GoriyaState { Walking, Throwing }

        private const int HEALTH = 3;
        private const int DAMAGE = 1;
        private const float STEP_SIZE = 16f;
        private const float STEP_DELAY = 0.5f;
        private const float MOVE_SPEED = 100f;
        private const float THROW_COOLDOWN_MIN = 2.0f;
        private const float THROW_COOLDOWN_MAX = 4.0f;
        private const float BOOMERANG_SPEED = 3f;
        private const float BOOMERANG_RANGE = 400f;
        private const float FLIP_INTERVAL = 0.075f;

        private readonly NavigationContext navigation;
        private readonly Action<AbstractItem> spawnProjectile;

        private GoriyaState currentState;
        private GoriyaDirection currentDirection;
        private Vector2 targetPosition;
        private float stepTimer;
        private float throwTimer;
        private bool spriteHorizontalFlip;
        private float flipTimer;
        private Boomerang activeBoomerang;

        protected override bool FlipsOnVertical => true;

        public Goriya(Texture2D texture, Vector2 position,
            List<Sprint.Block.Block> solidBlocks, Rectangle innerBounds,
            Action<AbstractItem> spawnProjectile = null)
            : base(texture, position, HEALTH, DAMAGE)
        {
            // Introduce Parameter Object: solidBlocks and innerBounds collapsed into
            // NavigationContext, reducing constructor fan-out and making the pairing explicit.
            navigation = new NavigationContext(solidBlocks, innerBounds);
            this.spawnProjectile = spawnProjectile;

            currentState = GoriyaState.Walking;
            currentDirection = GoriyaDirection.Down;
            targetPosition = position;
            stepTimer = STEP_DELAY;
            flipTimer = FLIP_INTERVAL;
            throwTimer = GetRandomThrowTime();
            spriteHorizontalFlip = true;

            sprite = new DirectionalAnimatedSprite(texture, position,
                GoriyaDirection.Down.WalkFrames, 11, 16, 16, 0.2f, true);
            Rect = new Rectangle((int)position.X, (int)position.Y,
                16 * (int)GameServices.ScaleFactor, 16 * (int)GameServices.ScaleFactor);
        }

        protected override void UpdateEnemy(GameTime gameTime)
        {
            if (!isAlive) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentState == GoriyaState.Walking)
            {
                throwTimer -= dt;
                if (throwTimer <= 0)
                    ThrowBoomerang();
            }

            // Decompose Conditional: state dispatch is now clean and each branch
            // delegates to a focused private method with a single responsibility.
            switch (currentState)
            {
                case GoriyaState.Walking:  UpdateWalking(dt);  break;
                case GoriyaState.Throwing: UpdateThrowing(dt); break;
            }

            sprite.Update(gameTime);
        }

        private void UpdateWalking(float dt)
        {
            if (Vector2.Distance(Position, targetPosition) > 1f)
            {
                MoveTowardTarget(dt);
            }
            else
            {
                Position = targetPosition;
                stepTimer -= dt;
                if (stepTimer <= 0)
                {
                    ChooseNextStep();
                    stepTimer = STEP_DELAY;
                }
            }
        }

        // Extract Method: movement logic pulled out of UpdateWalking to reduce its
        // length and give the flip animation its own named scope.
        private void MoveTowardTarget(float dt)
        {
            Vector2 dir = targetPosition - Position;
            dir.Normalize();
            Position += dir * MOVE_SPEED * dt;

            if (FlipsOnVertical && IsVertical(currentDirection))
            {
                flipTimer -= dt;
                if (flipTimer <= 0)
                {
                    spriteHorizontalFlip = !spriteHorizontalFlip;
                    UpdateSprite();
                    flipTimer = FLIP_INTERVAL;
                }
            }
        }

        private void UpdateThrowing(float dt)
        {
            if (activeBoomerang == null || activeBoomerang.IsFinished)
            {
                activeBoomerang = null;
                currentState = GoriyaState.Walking;
                throwTimer = GetRandomThrowTime();
                UpdateSprite();
            }
        }

        private void ThrowBoomerang()
        {
            currentState = GoriyaState.Throwing;
            UpdateSprite();

            // Move Method: throw origin is now resolved by GoriyaDirection itself,
            // removing Feature Envy from this method.
            Vector2 origin = currentDirection.GetThrowOrigin(Position);
            Vector2 velocity = currentDirection.UnitVector * BOOMERANG_SPEED;

            activeBoomerang = ItemFactory.CreateEnemyBoomerang(origin, velocity, BOOMERANG_RANGE).StartMoving();
            spawnProjectile?.Invoke(activeBoomerang);
        }

        public override void Die()
        {
            base.Die();
            activeBoomerang?.Cancel();
            activeBoomerang = null;
        }

        private void ChooseNextStep()
        {
            Vector2 candidate = ChooseValidStep(navigation.SolidBlocks, navigation.InnerBounds, STEP_SIZE);
            if (candidate == Position) return;

            currentDirection = GetDirectionTo(candidate);
            targetPosition = candidate;
            flipTimer = FLIP_INTERVAL;
            UpdateSprite();
        }

        private GoriyaDirection GetDirectionTo(Vector2 target)
        {
            Vector2 diff = target - Position;
            if (MathF.Abs(diff.X) > MathF.Abs(diff.Y))
                return diff.X < 0 ? GoriyaDirection.Left : GoriyaDirection.Right;
            else
                return diff.Y < 0 ? GoriyaDirection.Up : GoriyaDirection.Down;
        }

        // Extract Method: vertical direction check extracted to remove repeated
        // inline comparisons and give the concept a readable name.
        private static bool IsVertical(GoriyaDirection dir) =>
            dir.UnitVector.X == 0;

        private void UpdateSprite()
        {
            var dirSprite = sprite as DirectionalAnimatedSprite;

            // Decompose Conditional: previously three nested if/switch blocks.
            // Now each branch is a single line delegating to GoriyaDirection's
            // own frame data, eliminating Switch Statement Duplication entirely.
            int[] frames = currentState == GoriyaState.Throwing
                ? currentDirection.ThrowFrames
                : currentDirection.WalkFrames;

            bool flip = currentState == GoriyaState.Throwing && currentDirection.Equals(GoriyaDirection.Left)
                ? true
                : currentDirection.FlipSprite && spriteHorizontalFlip;

            dirSprite?.UpdateFrames(frames, flip);
        }

        private float GetRandomThrowTime() =>
            GetRandomFloat(THROW_COOLDOWN_MIN, THROW_COOLDOWN_MAX);
    }
}