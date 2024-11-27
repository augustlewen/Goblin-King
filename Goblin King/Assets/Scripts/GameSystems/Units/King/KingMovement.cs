using System;
using System.Collections;
using GameSystems.Units.AI;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Units.King
{
    public class KingMovement : MonoBehaviour
    {
        public static KingMovement i;
        [HideInInspector] public UnityEvent OnMoveUpdate = new();
        public float speed;

        public BoxCollider navArea;
        
        [Header("Movement Update")]
        public float updateRate = 0.1f;
        public float movementThreshold = 3;
        private Vector3 worldAnchor;
        

        private void Awake()
        {
            i = this;
            StartCoroutine(CheckPlayerMovement());
        }

        private void Update()
        {
            var xMove = Input.GetAxisRaw("Horizontal");
            var yMove = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(xMove, yMove) * (speed * Time.deltaTime);
            
            if (xMove != 0)
                transform.localScale = new Vector3(xMove, 1, 1);
        }
        
        
        private IEnumerator CheckPlayerMovement()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);

            while (true)
            {
                if (Vector3.Distance(worldAnchor, transform.position) > movementThreshold)
                {
                    worldAnchor = transform.position;
                    OnMoveUpdate.Invoke();
                }

                yield return wait;
            }
        }
    }
}
