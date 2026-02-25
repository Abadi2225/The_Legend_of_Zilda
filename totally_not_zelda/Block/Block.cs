using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint.Block;

internal class Block : AbstractBlock
{
    private const string RESOURCE_NAME = "blocks/tiles";
    internal const int DEFAULT_TILE_WIDTH = 32;

    public Block(ContentManager content, Vector2 pos, Rectangle textureMask, int width = DEFAULT_TILE_WIDTH)
     : base(content, RESOURCE_NAME, pos, false)
    {
        Sprite = new TileSprite(
                texture,
                textureMask,
                pos,
                width
                );
    }
}
