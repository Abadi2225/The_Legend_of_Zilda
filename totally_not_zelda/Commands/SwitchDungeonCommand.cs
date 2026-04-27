using Sprint.Interfaces;

namespace Sprint.Commands
{
    public class SwitchDungeonCommand : ICommand
    {
        private GameplayState gameState;
        private int dungeonNumber;

        public SwitchDungeonCommand(IGameState gameState, int dungeonNumber)
        {
            this.gameState = (GameplayState)gameState;
            this.dungeonNumber = dungeonNumber;
        }

        public void Execute()
        {
            gameState.SwitchDungeon(dungeonNumber);
        }
    }
}
