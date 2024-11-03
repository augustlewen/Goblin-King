using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.World.Grid
{
    public class WorldGrid : MonoBehaviour
    {
        public static WorldGrid i;

        private Dictionary<Vector2Int, GridCell> grid;
        public float cellSize;
        

        private void Awake()
        {
            i = this;
            grid = new Dictionary<Vector2Int, GridCell>();
        }
        
        // Convert a world position to grid coordinates
        private Vector2Int WorldToGridPosition(Vector2 position)
        {
            int x = Mathf.FloorToInt(position.x / cellSize);
            int y = Mathf.FloorToInt(position.y / cellSize);
            return new Vector2Int(x, y);
        }

        // Get or create a cell at a specific grid position
        public GridCell GetOrCreateCell(Vector2 position)
        {
            Vector2Int gridPos = WorldToGridPosition(position);
        
            if (!grid.ContainsKey(gridPos))
            {
                grid[gridPos] = new GridCell();
            }
        
            return grid[gridPos];
        }

        // Add an object to the grid
        public void AddObject(GameObject obj)
        {
            Vector2Int gridPos = WorldToGridPosition(obj.transform.position);
            GridCell cell = GetOrCreateCell(gridPos);
            cell.AddObject(obj);
        }

        // Remove an object from the grid
        public void RemoveObject(GameObject obj)
        {
            Vector2Int gridPos = WorldToGridPosition(obj.transform.position);
            if (grid.TryGetValue(gridPos, out GridCell cell))
            {
                cell.RemoveObject(obj);
            }
        }

        public bool IsOccupied(Vector2 position)
        {
            return GetOrCreateCell(position).objectsInCell.Count > 0;
        }
    }
}