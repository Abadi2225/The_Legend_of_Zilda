using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint.Levels;

public class Level
{
    private int[,] tiles;
    private Texture2D tilesetTexture;
    private int tileSize = 16;

    public Level(int[,] tiles, Texture2D tileset)
    {
        this.tiles = tiles;
        this.tilesetTexture = tileset;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int width = 16;
        int height = 16;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int gid = tiles[x, y];
                if (gid == 0) continue;

                Rectangle source = GetSourceRectangle(gid);
                Vector2 position = new Vector2(x * tileSize, y * tileSize);

                spriteBatch.Draw(tilesetTexture, position, source, Color.White);
            }
        }
    }

    private Rectangle GetSourceRectangle(int gid)
    {
        int columns = tilesetTexture.Width / tileSize;

        int index = gid - 1; // Tiled IDs start at 1
        int sx = (index % columns) * tileSize;
        int sy = (index / columns) * tileSize;

        return new Rectangle(sx, sy, tileSize, tileSize);
    }
}
