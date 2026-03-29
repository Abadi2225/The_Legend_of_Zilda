using Sprint.Interfaces;
using Sprint.Commands;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sprint.InputHandling;

public class MenuInputHandler : IInputHandler
{
    private Dictionary<Keys, ICommand> commands = new Dictionary<Keys, ICommand>
    {
        {Keys.Q, new QuitCommand()},
        {Keys.Enter, new SetStateCommand(new GameplayState())}
    };

    public void HandleInput()
    {
        foreach (var command in commands)
        {
            if (GameServices.KeyInput.IsKeyPressed(command.Key))
            {
                command.Value.Execute();
            }
        }
    }
}