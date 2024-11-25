using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.Units.AI
{
    public class AIMovement : MonoBehaviour
    {
        // public int width; // Width in grid cells
        // public int height; // Height in grid cells
        // private AIGrid grid; // Reference to the grid
        // private Vector2Int gridPosition; // The current grid position
        //
        // private void Start()
        // {
        //     grid = AIGrid.i;
        //     gridPosition = grid.WorldToGridPosition(transform.position);
        //     Debug.Log(gridPosition);
        //     // // Calculate the grid position based on the world position and the grid's cell size
        //     // gridPosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x / grid.cellSize), Mathf.FloorToInt(worldPosition.y / grid.cellSize));
        //     //
        //     // // Adjust the gridPosition based on the grid's origin
        //     // gridPosition -= grid.origin; // Handle negative coordinates based on the grid's current origin
        // }
        //
        // public void Move(Vector2Int targetPosition)
        // {
        //     // Convert the current world position and target world position to grid positions
        //     Vector2Int startGridPosition = grid.WorldToGridPosition(transform.position);
        //     Vector2Int targetGridPosition = grid.WorldToGridPosition(new Vector3(targetPosition.x, targetPosition.y, 0));
        //     Debug.Log(targetGridPosition);
        //
        //     // Find the path to the target position directly within this method
        //     List<Cell> path = grid.FindPath(startGridPosition, targetGridPosition);
        //     if (path != null && path.Count > 0)
        //     {
        //         // Move character along the path
        //         StartCoroutine(MoveAlongPath(path));
        //     }
        // }
        //
        // private IEnumerator MoveAlongPath(List<Cell> path)
        // {
        //     // Iterate through the path
        //     foreach (Cell cell in path)
        //     {
        //         // Update the grid position to the current cell's grid-relative position
        //         gridPosition = cell.position;
        //         transform.position = grid.GridToWorldPosition(gridPosition);
        //
        //         // Optionally, wait for a short time or until the character reaches the position
        //         yield return new WaitForSeconds(0.1f); // Adjust this time as necessary
        //     }
        //
        //     // Once finished moving, occupy the area where the character is now located
        //     grid.OccupyArea(gridPosition, width, height);
        // }
    }
}