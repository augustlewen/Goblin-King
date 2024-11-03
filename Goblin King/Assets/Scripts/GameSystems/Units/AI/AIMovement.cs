using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.Units.AI
{
    public class AIMovement : MonoBehaviour
    {
        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false; // Prevents rotation in 2D
            agent.updateUpAxis = false;   // Ensures the agent stays in 2D plane
        }

        public void SetDestination(Vector2 targetPosition)
        {
            agent.SetDestination(new Vector3(targetPosition.x, targetPosition.y, 0));
        }
    }
}