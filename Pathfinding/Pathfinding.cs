using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Grid grid; // Reference to the grid

    // Finds a path from start to target cell
    public List<Cell> FindPath(Cell start, Cell target)
    {
        var toCheck = new List<Cell> { start };      // List of cells to check, starts with start cell
        var checkedCells = new HashSet<Cell>();      // Set of already checked cells
        var cameFrom = new Dictionary<Cell, Cell>(); // Tracks where each cell came from
        var costs = new Dictionary<Cell, float> { { start, 0 } }; // Cost to reach each cell, start is 0

        while (toCheck.Count > 0)                    // Keep going while there are cells to check
        {
            Cell current = toCheck[0];               // Grab the first cell to process
            if (current == target)                   // If we reached the target
                return BuildPath(start, target, cameFrom); // Build and return the path

            toCheck.Remove(current);                 // Remove current cell from toCheck
            checkedCells.Add(current);               // Mark it as checked

            foreach (Cell neighbor in grid.GetNeighborCells(current)) // Check all neighbors
            {
                if (neighbor == null || neighbor.IsBlocked || checkedCells.Contains(neighbor))
                    continue;                        // Skip if neighbor is null, blocked, or already checked

                float cost = costs[current] + 1;     // Calculate cost to reach neighbor (current cost + 1)
                if (!costs.ContainsKey(neighbor) || cost < costs[neighbor]) // If neighbor is new or cost is lower
                {
                    costs[neighbor] = cost;          // Update cost to reach neighbor
                    cameFrom[neighbor] = current;    // Record where we came from
                    toCheck.Add(neighbor);           // Add neighbor to check later
                }
            }
        }
        return null;                                 // Return null if no path found
    }

    // Builds the path from start to end using cameFrom
    List<Cell> BuildPath(Cell start, Cell end, Dictionary<Cell, Cell> cameFrom)
    {
        var path = new List<Cell>();                 // List to store the path
        Cell current = end;                          // Start from the end cell
        while (current != start)                     // Loop until we reach the start
        {
            path.Add(current);                       // Add current cell to path
            current = cameFrom[current];             // Move to the previous cell
        }
        path.Add(start);                             // Add the start cell
        path.Reverse();                              // Reverse to get path from start to end
        for (int e = 1; e < path.Count - 1; e++)     // Color all cells except start and end
            path[e].Paint(CellType.Path);            // Paint them as path (yellow)
        return path;                                 // Return the completed path
    }
}