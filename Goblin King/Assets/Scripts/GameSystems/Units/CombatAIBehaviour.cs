using System;
using System.Collections;
using GameSystems.Units.AI;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Units
{
    public class CombatAIBehaviour : MonoBehaviour
    {
        public UnitStats targetStats;

        private UnitStats myStats;
        private AINavMovement navMovement;

        private bool isInRangeOfTarget;
        private float attackCooldown;
        
        int attackDamage;
        float attackRange;
        float attackRate;
        private GameObject projectile;
        
        private void Awake()
        {
            navMovement = GetComponent<AINavMovement>();
            navMovement.OnReachDestination.AddListener(OnReachDestination);
            
        }

        IEnumerator AttackBehaviour()
        {
            while (targetStats != null)
            {
                if (!IsInRangeOfTarget())
                {
                    navMovement.SetDestination(targetStats.transform.position, attackRange);
                    yield return new WaitForSeconds(0.5f);
                }

                if (attackCooldown > 0)
                    yield return new WaitForSeconds(0.05f);

                StartCoroutine(Attack());
            }
        }

        IEnumerator Attack()
        {
            attackCooldown = attackRate;

            yield return new WaitForSeconds(0.2f);

            if (projectile != null)
                Instantiate(projectile, transform.position, quaternion.identity);
            else
                targetStats.OnTakeDamage(attackDamage);
        }
        
        // private void Update()
        // {
        //     if(targetStats == null)
        //         return;
        //
        //     if (IsInRangeOfTarget())
        //     {
        //     }
        //     else
        //     {
        //         navMovement.SetDestination(targetStats.transform.position, attackRange);
        //     }
        //     
        // }
        //
        

        bool IsInRangeOfTarget()
        {
            float distance = Vector2.Distance(transform.position, targetStats.transform.position);
            return distance <= attackRange;
        }

        private void OnReachDestination()
        {
            
        }

        public void SetTarget(UnitStats stats)
        {
            targetStats = stats;
            StartCoroutine(AttackBehaviour());
            // navMovement.SetDestination(stats.transform.position, myStats.attackRange);
        }


        
    }
}