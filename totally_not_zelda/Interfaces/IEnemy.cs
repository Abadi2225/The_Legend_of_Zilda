namespace Sprint.Interfaces;

public interface IEnemy : IPositionedSprite
    {
        int Health { get; set; }
        int MaxHealth { get; }
        int Damage { get; }

        bool IsAlive { get; }

        public void TakeDamage(int damageAmount);
        public void Die();
        public void Reset();
    }