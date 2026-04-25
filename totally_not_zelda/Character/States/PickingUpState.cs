using Microsoft.Xna.Framework;

namespace Sprint.Character.States;

internal class PickingUpState : LinkState
{
    public bool IsTriforce { get; private set; }
    public Rectangle ItemRect { get; private set; }
    public double Timer { get; private set; }

    public bool IsTriforceComplete => IsTriforce && Timer >= 10;

    public void InitWeapon(Rectangle itemRect)
    {
        IsTriforce = false;
        ItemRect = itemRect;
        Timer = 0;
    }

    public void InitTriforce(Rectangle itemRect)
    {
        IsTriforce = true;
        ItemRect = itemRect;
        Timer = 0;
    }

    public override void OnEnter(Link link)
    {
        link.Move = Vector2.Zero;
        Timer = 0;

        if (IsTriforce)
        {
            link.PickUpTriforce.Reset();
            link.Sprite = link.PickUpTriforce;
        }
        else
        {
            link.Direction = Directions.Up;
            link.PickUpWeapon.Reset();
            link.Sprite = link.PickUpWeapon;
        }
    }

    public override void Update(Link link, LinkStateMachine sm, GameTime gameTime)
    {
        if (IsTriforce)
            Timer += gameTime.ElapsedGameTime.TotalSeconds;

        link.Sprite.Update(gameTime);
    }
}
