using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] Grid grid;               // Reference to the grid
    [SerializeField] Pathfinding pathfinding; // Reference to pathfinding logic

    Cell startCell, endCell;             // Cells for start and end points

    bool hasStart;                       // Tracks if start point is set

    void Update()
    {
        // Check for mouse clicks (left, right, or middle)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert mouse position to world coordinates
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);           // Cast a 2D ray from mouse position

            if (hit.collider != null)        // If ray hits something
            {
                Cell clickedCell = hit.collider.GetComponent<Cell>(); // Get the clicked cell
                if (clickedCell == null) return; // Exit if no Cell component found

                if (Input.GetMouseButtonDown(0)) // Left click: set start point
                {
                    startCell = clickedCell;         // Assign start cell
                    startCell.Paint(CellType.Start); // Paint it as start (blue)
                    hasStart = true;                 // Mark start as set
                }
                if (Input.GetMouseButtonDown(1) && hasStart) // Right click: set end point and find path
                {
                    endCell = clickedCell;       // Assign end cell
                    endCell.Paint(CellType.End); // Paint it as end (red)
                    List<Cell> path = pathfinding.FindPath(startCell, endCell); // Calculate path
                }
                if (Input.GetMouseButtonDown(2))       // Middle click: toggle wall
                    clickedCell.Block(BlockType.Wall); // Set cell as wall (red)
            }
        }
        if (Input.GetKeyDown(KeyCode.E))     // Press 'E' to reset grid
            grid.ReloadCells();              // Reset all cells to default
    }
}