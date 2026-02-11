using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace sprint0;

public class KeyboardController : IController
{
    private readonly Dictionary<Keys, ICommand> _bindings = new();
    private KeyboardState _prev;

    public void Bind(Keys key, ICommand command)
    {
        _bindings[key] = command;
    }

    public void Update()
    {
        KeyboardState cur = Keyboard.GetState();

        foreach (var pair in _bindings)
        {
            Keys key = pair.Key;

            if (cur.IsKeyDown(key) && _prev.IsKeyUp(key))
            {
                pair.Value.Execute();
            }
        }

        _prev = cur;
    }
}
