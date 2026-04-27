using Microsoft.Xna.Framework;
using Sprint.Character.States;
using Sprint.Interfaces;

namespace Sprint.Character;

internal class LinkStateMachine
{
    private readonly Link link;
    private ILinkState currentState;

    private readonly IdleState idle = new();
    private readonly WalkingState walking = new();
    private readonly AttackingState attacking = new();
    private readonly UsingItemState usingItem = new();
    private readonly PickingUpState pickingUp = new();
    private readonly DamagedState damaged = new();
    private readonly DeadState dead = new();
    private readonly GrabbedState grabbed = new();

    private float damageCooldown;
    private const float DAMAGE_COOLDOWN_DURATION = 3f;
    private const double BLINK_INTERVAL = 0.10;

    public bool IsAttacking => currentState is AttackingState;
    public bool AttackHitLanded { get; set; }

    public bool IsDead => currentState is DeadState;

    public bool IsGrabbed
    {
        get => currentState is GrabbedState;
        set
        {
            if (value && currentState is not GrabbedState)
                TransitionTo(grabbed);
            else if (!value && currentState is GrabbedState)
                TransitionTo(idle);
        }
    }

    public bool IsPushing => currentState is WalkingState && walking.IsPushing;

    public bool TriforceActive => currentState is PickingUpState && pickingUp.IsTriforce;
    public double TriforceTimer => currentState is PickingUpState ? pickingUp.Timer : 0;

    public bool DeathSequenceFinished => currentState is DeadState && dead.IsFinished;
    public bool DeathBackgroundBlack => currentState is DeadState && dead.IsBackgroundBlack;
    public bool IsSparkleStage => currentState is DeadState && dead.IsSparkle;

    public bool IsVisible => damageCooldown <= 0 || (int)(damageCooldown / BLINK_INTERVAL) % 2 == 0;

    public Rectangle? PickUpItemRect => currentState is PickingUpState ? pickingUp.ItemRect : null;
    public bool IsTriforcePickup => currentState is PickingUpState && pickingUp.IsTriforce;


    public LinkStateMachine(Link link)
    {
        this.link = link;
        currentState = idle;
        idle.OnEnter(link);
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (damageCooldown > 0)
            damageCooldown -= dt;
        currentState.Update(link, this, gameTime);
    }

    private void TransitionTo(ILinkState newState)
    {
        currentState.OnExit(link);
        currentState = newState;
        currentState.OnEnter(link);
    }

    internal void TransitionToIdle() => TransitionTo(idle);

    public void HandleSetMove(Directions dir)
    {
        if (currentState is AttackingState or UsingItemState or PickingUpState
                or DeadState or GrabbedState) return;

        if (currentState is DamagedState)
        {
            damaged.SetMove(dir, link);
            return;
        }

        if (currentState is WalkingState)
            walking.SetDirection(dir, link);
        else
        {
            TransitionTo(walking);
            walking.SetDirection(dir, link);
        }
    }

    public void HandleStopMove()
    {
        if (currentState is DamagedState)
        {
            damaged.StopMove(link);
            return;
        }

        if (currentState is WalkingState)
            TransitionTo(idle);
    }

    public void HandleStartAttack()
    {
        if (currentState is AttackingState or DeadState or GrabbedState) return;

        AttackHitLanded = false;
        TransitionTo(attacking);
    }

    public void HandleStartUseItem()
    {
        if (currentState is UsingItemState or DeadState or GrabbedState) return;

        TransitionTo(usingItem);
    }

    public void HandleStartPickUpWeapon(Rectangle itemRect)
    {
        if (currentState is UsingItemState or AttackingState or DeadState or GrabbedState) return;

        pickingUp.InitWeapon(itemRect);
        TransitionTo(pickingUp);
    }

    public void HandleStartPickUpTriforce(Rectangle itemRect)
    {
        if (currentState is UsingItemState or AttackingState or DamagedState
                         or DeadState or GrabbedState) return;

        pickingUp.InitTriforce(itemRect);
        TransitionTo(pickingUp);
    }

    public void HandleTakeDamage(int amount)
    {
        if (currentState is DeadState or GrabbedState) return;
        if (damageCooldown > 0) return;

        int newHealth = MathHelper.Clamp(link.Health - amount, 0, link.MaxHealth);
        link.SetHealth(newHealth);
        damageCooldown = DAMAGE_COOLDOWN_DURATION;

        if (newHealth <= 0)
            TransitionTo(dead);
        else
            TransitionTo(damaged);
    }

    public void HandleStartDamaged()
    {
        if (currentState is DamagedState or DeadState) return;
        TransitionTo(damaged);
    }

    public void HandleStartDeath()
    {
        if (currentState is DeadState) return;
        TransitionTo(dead);
    }

    public void HandleStartPush()
    {
        if (currentState is WalkingState)
            walking.StartPushing();
    }

    public bool ShouldEndTriforceSequence() =>
        currentState is PickingUpState && pickingUp.IsTriforceComplete;

    public void HandleEndTriforceSequence()
    {
        if (currentState is PickingUpState)
            TransitionTo(idle);
    }

    internal void OnAttackFinished() => TransitionTo(idle);

    internal void OnUseItemFinished() => TransitionTo(idle);

    internal void OnPickUpFinished()
    {
        if (currentState is PickingUpState && pickingUp.IsTriforce) return;
        TransitionTo(idle);
    }
}
