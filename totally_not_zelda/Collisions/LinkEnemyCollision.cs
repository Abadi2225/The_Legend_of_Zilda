using Sprint.Character;
using Sprint.Enemies;
using Sprint.Interfaces;

namespace Sprint.Collision;

internal class LinkEnemyCollision : ICollisionHandler
{
    private readonly Link link;
    private readonly EnemyManager enemyManager;

    public LinkEnemyCollision(Link link, EnemyManager enemyManager)
    {
        this.link = link;
        this.enemyManager = enemyManager;
    }

    public void Handle()
    {   //Check collision between Link and each enemy.
        foreach (var enemy in enemyManager.enemyList)
        {
            if (!enemy.IsAlive) return;
            if (link.Rect.Intersects(enemy.Rect))
                link.TakeDamage(enemy.Damage);
        }
    }
}
