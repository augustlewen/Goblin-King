using System;
using GameSystems.Units.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GameSystems.Units.Goblins
{
    public class GoblinAI : AINavMovement
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                // Move(new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y));
                SetDestination(mouseWorldPosition);
            }
        }
    }
}