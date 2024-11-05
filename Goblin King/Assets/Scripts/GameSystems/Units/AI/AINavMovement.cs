using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.Units.AI
{
    public class AINavMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        
        private AIGrid grid; // Reference to the grid
        private Vector2Int gridPosition; // The current grid position

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false; // Prevents rotation in 2D
            agent.updateUpAxis = false; // Ensures the agent stays in 2D plane
        }

        protected void SetDestination(Vector2 pos)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(pos, path);

            if (path.status == NavMeshPathStatus.PathComplete)
                agent.SetDestination(pos);
            else
                Debug.LogWarning("Destination not reachable");
        }
    }
}