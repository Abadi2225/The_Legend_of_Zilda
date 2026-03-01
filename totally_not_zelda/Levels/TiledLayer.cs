namespace Sprint.Layers;

public class TiledLayer
{
    public string name { get; set; }
    public string type { get; set; }

    public int width { get; set; }
    public int height { get; set; }

    public int[] data { get; set; }
}