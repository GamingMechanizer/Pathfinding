using UnityEngine;

public enum CellType { Normal, Start, End, Path } // Types for cell roles
public enum BlockType { None, Wall, Water, Rock } // Types for block states

public class Cell : MonoBehaviour
{
    [SerializeField] SpriteRenderer cellRenderer; // Renderer for this cell's sprite

    public int X { get; private set; }               // X position in grid
    public int Y { get; private set; }               // Y position in grid
    public bool IsBlocked { get; private set; }      // True if cell is a barrier
    public BlockType BlockType { get; private set; } // Type of block (if any)

    // Sets up the cell with position and coordinates
    public void Initialize(int x, int y, Vector3 position)
    {
        X = x;                                 // Assign X coordinate
        Y = y;                                 // Assign Y coordinate
        transform.position = position;         // Set world position
        IsBlocked = false;                     // Default: cell is passable
        BlockType = BlockType.None;            // Default: no block
        cellRenderer.color = Color.white;      // Default color: white
    }

    // Sets the cell as blocked with a specific block type
    public void Block(BlockType blockType)
    {
        IsBlocked = blockType != BlockType.None;   // Blocked if not "None"
        BlockType = blockType;                     // Set block type
        switch (blockType)                         // Color based on block type
        {
            case BlockType.None: cellRenderer.color = Color.white; break;  // No block: white
            case BlockType.Wall: cellRenderer.color = Color.gray; break;    // Wall: red
            case BlockType.Water: cellRenderer.color = Color.cyan; break;  // Water: cyan
            case BlockType.Rock: cellRenderer.color = Color.gray; break;   // Rock: gray
        }
    }

    // Colors the cell based on its role (start, end, path, etc.)
    public void Paint(CellType type)
    {
        if (IsBlocked) return;  // Skip if cell is blocked
        switch (type)
        {
            case CellType.Normal: cellRenderer.color = Color.white; break;  // Normal: white
            case CellType.Start: cellRenderer.color = Color.blue; break;    // Start: blue
            case CellType.End: cellRenderer.color = Color.red; break;       // End: red
            case CellType.Path: cellRenderer.color = Color.green; break;   // Path: yellow
        }
    }
}