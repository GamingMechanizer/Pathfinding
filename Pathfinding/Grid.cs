using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = new Vector2(10, 10);   // Grid size in units (width, height)
    [SerializeField] Cell cellPrefab;                          // Prefab for creating cells

    Cell[,] cells;                                             // 2D array to store all cells

    int gridWidth, gridHeight;                                 // Number of cells in width and height
    float cellSize = 1f;                                       // Size of each cell in units

    // Called when the object starts
    void Awake()
    {
        gridWidth = Mathf.RoundToInt(gridSize.x / cellSize);   // Calculate grid width in cells
        gridHeight = Mathf.RoundToInt(gridSize.y / cellSize);  // Calculate grid height in cells
        CreateGrid();                                          // Build the grid
    }

    // Creates the grid of cells
    void CreateGrid()
    {
        cells = new Cell[gridWidth, gridHeight];             // Initialize the cell array
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2; // Bottom-left corner of grid

        for (int x = 0; x < gridWidth; x++)                  // Loop through width
            for (int y = 0; y < gridHeight; y++)             // Loop through height
            {
                Vector3 worldPos = bottomLeft + Vector3.right * (x * cellSize + cellSize / 2) + Vector3.up * (y * cellSize + cellSize / 2); // Calculate cell position
                Cell cellObj = Instantiate(cellPrefab, worldPos, Quaternion.identity, transform); // Spawn cell from prefab
                cellObj.Initialize(x, y, worldPos);          // Set up cell with coordinates and position
                cells[x, y] = cellObj;                       // Store cell in array
            }
    }

    // Returns array of neighboring cells (up, down, left, right)
    public Cell[] GetNeighborCells(Cell cell)
    {
        Cell[] neighbors = new Cell[4];                      // Array for 4 possible neighbors
        int index = 0;                                       // Index to track filled slots

        if (cell.X + 1 < gridWidth) neighbors[index++] = cells[cell.X + 1, cell.Y];  // Add right neighbor if exists
        if (cell.X - 1 >= 0) neighbors[index++] = cells[cell.X - 1, cell.Y];         // Add left neighbor if exists
        if (cell.Y + 1 < gridHeight) neighbors[index++] = cells[cell.X, cell.Y + 1]; // Add top neighbor if exists
        if (cell.Y - 1 >= 0) neighbors[index++] = cells[cell.X, cell.Y - 1];         // Add bottom neighbor if exists

        return neighbors;                                    // Return neighbors (some may be null)
    }

    // Resets all cells to default state
    public void ReloadCells()
    {
        foreach (var item in cells)                          // Loop through all cells
        {
            item.Block(BlockType.None);                      // Remove any block (make passable)
            item.Paint(CellType.Normal);                     // Set color to normal (white)
        }
    }
}