using System;
using GameSystems.Units.AI;
using UnityEngine;

namespace GameSystems.Units.King
{
    public class KingMovement : MonoBehaviour
    {
        public float speed;

        private Vector3 lastPosition;
        private float movementThreshold = 0.5f;


        private void Awake()
        {
            // FindFirstObjectByType<AIGrid>().UpdateGrid(transform.position);
        }

        private void Update()
        {
            var xMove = Input.GetAxisRaw("Horizontal");
            var yMove = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(xMove, yMove) * (speed * Time.deltaTime);
            
            if (xMove != 0)
                transform.localScale = new Vector3(xMove, 1, 1);
            
            
            // Vector3 currentPosition = transform.position;
            // // Check if the player has moved a significant distance
            // if (Vector3.Distance(currentPosition, lastPosition) >= movementThreshold)
            // {
            //     // Update the grid's origin based on the player's position
            //     AIGrid.i.UpdateGrid(currentPosition);
            //
            //     // Update last position
            //     lastPosition = currentPosition;
            // }
        }
    }
}
