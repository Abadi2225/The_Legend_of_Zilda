using Microsoft.Xna.Framework;
using Sprint.Interfaces;
using Sprint.Enemies;
using Sprint.Levels;
using Sprint.Block;
using Sprint.Character;
using Sprint.UI;
using Sprint.Collision;

public class TriforceResetHandler
{
    private readonly ILink link;
    private readonly LevelLoader levelLoader;
    private readonly EnemyFactory enemyFactory;
    private readonly DoorManager doorManager;
    private readonly OuterDungeonWalls dungeonWalls;

    private Level currentLevel;
    private LevelData currentLevelData;

    public TriforceResetHandler(
        ILink link,
        LevelLoader levelLoader,
        EnemyFactory enemyFactory,
        DoorManager doorManager,
        OuterDungeonWalls dungeonWalls,
        ref Level currentLevel,
        ref LevelData currentLevelData)
    {
        this.link = link;
        this.levelLoader = levelLoader;
        this.enemyFactory = enemyFactory;
        this.doorManager = doorManager;
        this.dungeonWalls = dungeonWalls;
        this.currentLevel = currentLevel;
        this.currentLevelData = currentLevelData;
    }

    public void Update()
    {
        if (link.ShouldEndTriforceSequence())
        {
            // reload current level data
            currentLevelData = levelLoader.GetCurrentLevel();

            // rebuild level (fresh enemies/items/blocks)
            currentLevel = LevelBuilder.Build(
                currentLevelData,
                enemyFactory,
                dungeonWalls.InnerBounds
            );

            // reset doors (important if boss room changed state)
            doorManager.Reset(currentLevelData.doors, currentLevelData.doorTypes);

            // place Link at center (or spawn)
            link.Position = new Vector2(
                GameServices.GameWidth / 2,
                GameServices.GameHeight / 2
            );

            link.EndTriforceSequence();
        }
    }
}