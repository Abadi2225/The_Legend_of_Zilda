using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class DeadState : LinkState
{
    private enum Stage { Spinning, WhiteFlash, Sparkle, Finished }

    private Stage stage;
    private double stageTimer;

    public bool IsFinished => stage == Stage.Finished;
    public bool IsWhiteFlash => stage == Stage.WhiteFlash;
    public bool IsSparkle => stage == Stage.Sparkle;
    public bool IsBackgroundBlack => stage == Stage.WhiteFlash || stage == Stage.Sparkle || stage == Stage.Finished;

    public override void OnEnter(Link link)
    {
        stage = Stage.Spinning;
        stageTimer = 0;
        link.Move = Vector2.Zero;
        link.DeadSprite.Reset();
        link.Sprite = link.DeadSprite;
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        stageTimer += gameTime.ElapsedGameTime.TotalSeconds;

        switch (stage)
        {
            case Stage.Spinning:
                link.Sprite.Update(gameTime);
                if (link.DeadSprite.Finished)
                {
                    stage = Stage.WhiteFlash;
                    stageTimer = 0;
                    link.Direction = Directions.Down;
                    link.Sprite = link.IdleDown;
                }
                break;

            case Stage.WhiteFlash:
                if (stageTimer >= 0.2)
                {
                    stage = Stage.Sparkle;
                    stageTimer = 0;
                    link.DeathSparkleSprite.Reset();
                }
                break;

            case Stage.Sparkle:
                link.DeathSparkleSprite.Update(gameTime);
                if (stageTimer >= 0.3)
                {
                    stage = Stage.Finished;
                    stageTimer = 0;
                }
                break;
        }
    }
}
