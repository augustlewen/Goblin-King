using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.Units.AI
{
    public class AINavMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        
        private AIGrid grid; // Reference to the grid
        private Vector2Int gridPosition; // The current grid position
        private bool hasDestination;
        
        
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false; // Prevents rotation in 2D
            agent.updateUpAxis = false; // Ensures the agent stays in 2D plane
        }
        
        public virtual void Update()
        {
            if (hasDestination &&  HasReachedDestination())
            {
                OnReachDestination();
            }
        }

        public virtual void OnReachDestination()
        {
            hasDestination = false;
        }

        protected void SetDestination(Vector2 pos)
        {
            hasDestination = true;
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(pos, path);

            if (path.status == NavMeshPathStatus.PathComplete)
                agent.SetDestination(pos);
        }
        
        private bool HasReachedDestination()
        {
            // Return true if the agent has a destination, is not calculating a path,
            // and remainingDistance is very close to zero
            return !agent.pathPending && 
                   agent.remainingDistance <= agent.stoppingDistance && 
                   !agent.hasPath && 
                   !agent.isStopped;
        }
    }
}