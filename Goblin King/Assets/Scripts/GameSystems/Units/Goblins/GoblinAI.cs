using System;
using GameSystems.Units.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GameSystems.Units.Goblins
{
    public class GoblinAI : AIMovement
    {
        private void Awake()
        {
            Debug.Log("!");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                
                SetDestination(mouseWorldPosition);
            }
        }
    }
}