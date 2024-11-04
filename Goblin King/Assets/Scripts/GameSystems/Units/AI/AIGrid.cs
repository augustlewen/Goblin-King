using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Units.AI
{
    public class AIGrid : MonoBehaviour
    {
        private Cell[,] cells; // 2D array of cells
        public int width;
        public int height;

        public static AIGrid i;
        public float cellSize;
        public static Vector2Int origin; // This will represent the starting point of the grid
        private Vector2Int worldOffset;
        private void Awake()
        {
            i = this;
            cells = new Cell[width, height];

            // Define the grid's origin offset (center of the grid)
            origin = new Vector2Int(width / 2, height / 2);

            // Initialize cells
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    cells[x, y] = new Cell(new Vector2Int(x, y));
        }
        
        
        // Method to update the grid origin based on the player's position
        public void UpdateGrid(Vector3 playerPosition)
        {
            // Compute the player's grid-based position relative to the grid's cell size
            Vector2Int playerGridPosition = new Vector2Int(
                Mathf.FloorToInt(playerPosition.x / cellSize),
                Mathf.FloorToInt(playerPosition.y / cellSize)
            );

            // Update the offset to center the grid on the player
            worldOffset = playerGridPosition - origin;
            Debug.Log("World Offset: " + worldOffset);
        }

        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        {
            Vector2Int gridPosition = new Vector2Int(
                Mathf.FloorToInt(worldPosition.x / cellSize),
                Mathf.FloorToInt(worldPosition.y / cellSize)
            );

            // Adjust for the world offset to find the correct cell in the grid
            return gridPosition - worldOffset;
        }

        public Vector3 GridToWorldPosition(Vector2Int gridPosition)
        {
            return new Vector3(
                (gridPosition.x + AIGrid.i.worldOffset.x) * cellSize,
                (gridPosition.y + AIGrid.i.worldOffset.y) * cellSize,
                0
            );
        }
        
        public List<Cell> FindPath(Vector2Int start, Vector2Int target)
        {
            List<Cell> path = new List<Cell>();

            // A simple implementation of a BFS or DFS pathfinding algorithm
            // This is a very basic example; you may want to implement a more sophisticated algorithm like A*

            Queue<Cell> queue = new Queue<Cell>();
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            queue.Enqueue(cells[start.x, start.y]);
            visited.Add(start);

            // Using a simple parent tracking dictionary to reconstruct the path later
            Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        
            while (queue.Count > 0)
            {
                Cell current = queue.Dequeue();
            
                // Check if we have reached the target
                if (current.position == target)
                {
                    // Reconstruct the path
                    Vector2Int step = target;
                    while (cameFrom.ContainsKey(step))
                    {
                        path.Add(cells[step.x, step.y]);
                        step = cameFrom[step];
                    }
                    path.Reverse(); // Reverse the path to get the correct order
                    return path;
                }

                // Explore neighbors
                foreach (var neighbor in GetWalkableNeighbors(current))
                {
                    if (!visited.Contains(neighbor.position))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor.position);
                        cameFrom[neighbor.position] = current.position; // Track the path
                    }
                }
            }

            return null; // No path found
        }

        
        private List<Cell> GetWalkableNeighbors(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            // Check adjacent cells (up, down, left, right, and diagonals)
            Vector2Int[] directions = {
                new (1, 0),  // Right
                new (-1, 0), // Left
                new (0, 1),  // Up
                new (0, -1), // Down
                new (1, 1),  // Up-Right
                new (-1, 1), // Up-Left
                new (1, -1), // Down-Right
                new (-1, -1) // Down-Left
            };

            foreach (var direction in directions)
            {
                Vector2Int neighborPos = cell.position + direction;

                // Ensure the neighbor is within bounds and walkable
                if (neighborPos.x >= 0 && neighborPos.x < width &&
                    neighborPos.y >= 0 && neighborPos.y < height &&
                    cells[neighborPos.x, neighborPos.y].isWalkable)
                {
                    neighbors.Add(cells[neighborPos.x, neighborPos.y]);
                }
            }

            return neighbors;
        }

        public void OccupyArea(Vector2Int position, int w, int h)
        {
            for (int x = position.x; x < position.x + w; x++)
            {
                for (int y = position.y; y < position.y + h; y++)
                {
                    cells[x, y].isWalkable = false; // Mark as occupied
                }
            }
        }
    }

    public class Cell
    {
        public bool isWalkable = true; // Can be walked on
        public Vector2Int position; // Position in the grid

        public Cell(Vector2Int position)
        {
            this.position = position;
        }
    }
    
}