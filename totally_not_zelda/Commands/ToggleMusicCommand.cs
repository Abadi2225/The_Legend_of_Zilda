using Sprint.Interfaces;
using Sprint.Sound;

namespace Sprint.Commands
{
    public class ToggleMusicCommand : ICommand
    {

        public void Execute()
        {
            MusicPlayer.ToggleMute();
        }
    }
}
