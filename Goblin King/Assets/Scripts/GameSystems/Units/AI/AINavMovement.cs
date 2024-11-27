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

        [FormerlySerializedAs("combatAIBehaviour")] [HideInInspector]public CombatAIBehaviour combatAI;
        
        
        protected virtual void Start()
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