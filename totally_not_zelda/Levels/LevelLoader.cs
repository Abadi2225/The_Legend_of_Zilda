using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Sprint.Levels;
using Microsoft.Xna.Framework;

public class LevelLoader
{
    // todo remove this
    private List<string> levels = new List<string>{
        "template",
        "12statues",
        "12blocks",
        "6blocks",
        "4blockssmall",
        "4blocksbig",
        "1block",
        "dice5",
        "wave",
        "dumbbell",
        "blockedstairs",
        "waffle",
        "boss",
        "pacman"
    };
    private int currentLevel = 0;
    public LevelData CycleNext()
    {
        if (currentLevel < levels.Count - 1)
        {
            currentLevel++;
        }
        return Load(levels[currentLevel]);
    }
    public LevelData CyclePrevious()
    {
        if (currentLevel > 0)
        {
            currentLevel--;
        }
        return Load(levels[currentLevel]);
    }
    public LevelData GetCurrentLevel()
    {
        return Load(levels[currentLevel]);
    }

    public LevelData Load(string levelName)
    {
        string path = $"Content/rooms/{levelName}.json";

        string json = File.ReadAllText(path);

        LevelData data =
            JsonSerializer.Deserialize<LevelData>(json);

        return data;
    }
}
