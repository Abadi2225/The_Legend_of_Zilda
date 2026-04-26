using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Enemies;
using Sprint.Interfaces;
using Sprint.Levels;
using Sprint.UI;
using System;
using Sprint.Item;
using Sprint.Doors;

namespace Sprint.GameStates
{
    internal class RoomManager
    {
        private readonly LevelLoader levelLoader;
        private readonly EnemyFactory enemyFactory;
        private readonly UIManager uiManager;
        private readonly OuterDungeonWalls dungeonWalls;
        private readonly Texture2D staircaseTexture;
        private readonly Action onRoomChanged;
        private readonly Action<AbstractItem> spawnItem;

        private IUIElement currentBackground;

        public Level CurrentLevel { get; private set; }
        public LevelData CurrentLevelData { get; private set; }
        public string CurrentLevelName => levelLoader.GetCurrentLevelName();

        public bool IsUnderground => CurrentLevelData?.background == "Underground";
        public bool IsNPCRoom => CurrentLevelName == "NPC";

        public Rectangle GetInnerBounds()
        {
            if (IsUnderground && currentBackground is StaircaseBackground sb)
                return sb.InnerBounds;
            return dungeonWalls.InnerBounds;
        }

        public RoomManager(
            LevelLoader levelLoader,
            EnemyFactory enemyFactory,
            UIManager uiManager,
            OuterDungeonWalls dungeonWalls,
            Texture2D staircaseTexture,
            Action onRoomChanged,
            Action<AbstractItem> spawnItem)
        {
            this.levelLoader      = levelLoader;
            this.enemyFactory     = enemyFactory;
            this.uiManager        = uiManager;
            this.dungeonWalls     = dungeonWalls;
            this.staircaseTexture = staircaseTexture;
            this.onRoomChanged    = onRoomChanged;
            this.spawnItem        = spawnItem;

            CurrentLevelData = levelLoader.GetCurrentLevel();
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
        }

        public void LoadRoom(string roomName)
        {
            CurrentLevelData = LevelLoader.Load(roomName);
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void LoadRoom(LevelData data)
        {
            CurrentLevelData = data;
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void CycleNext()
        {
            CurrentLevelData = levelLoader.CycleNext();
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void CyclePrevious()
        {
            CurrentLevelData = levelLoader.CyclePrevious();
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void ResetToFirst()
        {
            levelLoader.ResetToFirst();
            CurrentLevelData = levelLoader.GetCurrentLevel();
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void SwitchDungeon(int dungeon)
        {
            if (GameServices.CurrentDungeon == dungeon) return;
            GameServices.CurrentDungeon = dungeon;
            CurrentLevelData = levelLoader.ResetForDungeon();
            dungeonWalls.RefreshColor();
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();
            onRoomChanged?.Invoke();
        }

        public void HandleStairTransition(string targetRoom, DoorManager doorManager, ILink link)
        {
            LevelData newData = LevelLoader.Load(targetRoom);
            doorManager.Reset(newData.doors, newData.doorTypes, newData.doorOffsets, targetRoom);
            CurrentLevelData = newData;
            UpdateBackground();
            CurrentLevel = BuildCurrentLevel();

            link.Position = targetRoom == "blockedstairs"
                ? new Vector2(
                    5 * 16 * GameServices.ScaleFactor + GetInnerBounds().Left,
                    3 * 16 * GameServices.ScaleFactor + GetInnerBounds().Top)
                : new Vector2(
                    49 * GameServices.ScaleFactor + GetInnerBounds().Left,
                    GetInnerBounds().Top + 16 * GameServices.ScaleFactor);

            onRoomChanged?.Invoke();
        }

        private Level BuildCurrentLevel() =>
            LevelBuilder.Build(CurrentLevelData, enemyFactory, GetInnerBounds(), spawnItem);

        private void UpdateBackground()
        {
            if (currentBackground != null)
                uiManager.RemoveElement(currentBackground);

            currentBackground = IsUnderground
                ? (IUIElement)new StaircaseBackground(staircaseTexture)
                : dungeonWalls;

            uiManager.AddElement(currentBackground);
        }
    }
}