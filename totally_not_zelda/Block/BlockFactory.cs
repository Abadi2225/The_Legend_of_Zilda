using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Sprint.Interfaces;
using Sprint.Levels;

namespace Sprint.Block;

public static class BlockFactory
{
    private const int SHEET_COLUMNS = 4;
    private const int TILE_SIZE = 16;
    private const int TILE_SPACING = 1;
    private enum BlockType
    {
        Blank,
        Square,
        StatueRight,
        StatueLeft,
        Black,
        Sand,
        Water,
        Stairs,
        Bricks,
        Ladder
    }

    public static Block Create(int tileId, Vector2 pos)
    {
        return tileId switch
        {
            0 => CreateBlock(BlockType.Blank, pos),
            1 => CreateBlock(BlockType.Square, pos),
            2 => CreateBlock(BlockType.StatueRight, pos),
            3 => CreateBlock(BlockType.StatueLeft, pos),
            4 => CreateBlock(BlockType.Black, pos),
            5 => CreateBlock(BlockType.Sand, pos),
            6 => CreateBlock(BlockType.Water, pos),
            7 => CreateBlock(BlockType.Stairs, pos),
            8 => CreateBlock(BlockType.Bricks, pos),
            9 => CreateBlock(BlockType.Ladder, pos),
            _ => null,
        };
    }

    private static Block CreateBlock(BlockType type, Vector2 pos)
    {
        int tileX = (int)type % SHEET_COLUMNS;
        int tileY = (int)type / SHEET_COLUMNS;

        Rectangle textureMask = new Rectangle(
            tileX * (TILE_SIZE + TILE_SPACING), // X position on the tile sheet
            tileY * (TILE_SIZE + TILE_SPACING), // Y position on the tile sheet
            TILE_SIZE,                          // Width of the tile
            TILE_SIZE                           // Height of the tile
        );

        bool walkable = type switch
        {
            BlockType.Blank or 
            BlockType.Sand or 
            BlockType.Stairs => true,
            _ => false  // Square, Statues, Black, Water, Bricks, Ladder all block movement
        };


		bool pushable = type switch
		{
			BlockType.StatueRight => true,  // StatueRight can be pushed
			_ => false  // For now, no blocks are pushable by default. This can be updated in the future if needed.
		};

        System.Console.WriteLine($"Creating block of type {type} at position {pos} with walkable={walkable} and pushable={pushable}");
		return new Block(GameServices.TileSheet, pos, textureMask, walkable, pushable);
    }
}
