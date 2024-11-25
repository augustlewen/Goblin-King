using System;
using GameSystems.Units.AI;
using UnityEngine;

namespace GameSystems.Units
{
    public class CombatAIBehaviour : MonoBehaviour
    {
        public UnitStats targetStats;

        private UnitStats myStats;
        private AINavMovement navMovement;
        
        private void Awake()
        {
            navMovement = GetComponent<AINavMovement>();
            navMovement.OnReachDestination.AddListener(OnReachDestination);
        }

        private void Update()
        {
            if(targetStats == null)
                return;
            
            
        }


        private void OnReachDestination()
        {
            
        }

        public void SetTarget(UnitStats stats)
        {
            targetStats = stats;

            float distance = Vector2.Distance(transform.position, targetStats.transform.position);

            navMovement.SetDestination(stats.transform.position, myStats.attackRange);
        }
    }
}