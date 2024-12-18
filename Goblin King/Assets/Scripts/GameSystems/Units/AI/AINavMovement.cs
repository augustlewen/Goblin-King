using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GameSystems.Units.AI
{
    public class AINavMovement : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnReachDestination = new();
        private NavMeshAgent agent;
        
        private AIGrid grid; // Reference to the grid
        private Vector2Int gridPosition; // The current grid position
        private bool hasDestination;

        [HideInInspector]public CombatAIBehaviour combatAI;
        private const float acceptanceRadius = 1.25f;


        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false; // Prevents rotation in 2D
            agent.updateUpAxis = false; // Ensures the agent stays in 2D plane
            
            combatAI = gameObject.AddComponent<CombatAIBehaviour>();
        }
        
        public virtual void Update()
        {
            if (hasDestination &&  HasReachedDestination())
            {
                ReachedDestination();
            }
        }

        protected virtual void ReachedDestination()
        {
            hasDestination = false;
            OnReachDestination.Invoke();
        }

        public void SetDestination(Vector2 targetPosition, float offsetDistance)
        {
            Vector2 directionToKing = (targetPosition - (Vector2)transform.position).normalized;

            Vector2 offset = directionToKing * offsetDistance;
            SetDestination(targetPosition - offset);
        }

        private void SetDestination(Vector2 pos)
        {
            hasDestination = true;
            // NavMeshPath path = new NavMeshPath();
            // NavMesh.SamplePosition(pos, out NavMeshHit hit, acceptanceRadius, NavMesh.AllAreas);
            // agent.CalculatePath(hit.position, path);

            if(!CanMoveTo(pos))
                return;
            
            if (GetSetPath(pos).status == NavMeshPathStatus.PathComplete)
                agent.SetDestination(pos);
        }

        private NavMeshPath GetSetPath(Vector2 pos)
        {
            var path = new NavMeshPath();
            bool validPosition = NavMesh.SamplePosition(pos, out NavMeshHit hit, acceptanceRadius, NavMesh.AllAreas);

            if (!validPosition)
                return null; // No valid position within the range

            return !agent.CalculatePath(hit.position, path) ? null : path;
        }
        
        public bool CanMoveTo(Vector2 pos)
        {
            // // Find a valid position on the NavMesh near the target position
            // bool validPosition = NavMesh.SamplePosition(pos, out NavMeshHit hit, acceptanceRadius, NavMesh.AllAreas);
            //
            // if (!validPosition)
            //     return false; // No valid position within the range

            // Validate the path to the sampled position
            // NavMeshPath path = new NavMeshPath();
            // bool pathExists = agent.CalculatePath(hit.position, path);
            var path = GetSetPath(pos);
            
            return path is { status: NavMeshPathStatus.PathComplete };
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