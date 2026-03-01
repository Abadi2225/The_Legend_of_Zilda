using System.IO;
using System.Text.Json;
using Sprint.Levels;
using Microsoft.Xna.Framework;

public class LevelLoader
{
    public LevelData Load(string levelName)
    {
        string path = $"Content/rooms/{levelName}.json";

        string json = File.ReadAllText(path);

        LevelData data =
            JsonSerializer.Deserialize<LevelData>(json);

        return data;
    }
}