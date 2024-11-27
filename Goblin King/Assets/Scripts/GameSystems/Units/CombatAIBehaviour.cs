using System.Collections;
using GameSystems.Units.AI;
using Specific_Items;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Units
{
    public class CombatAIBehaviour : MonoBehaviour
    {
        private UnitStats targetStats;

        private UnitStats myStats;
        private AINavMovement navMovement;

        private bool isInRangeOfTarget;
        private bool isAttackOnCooldown;
        
        int attackDamage;
        float attackRange;
        float attackRate;
        private Projectile projectilePrefab;
        
        private void Awake()
        {
            navMovement = GetComponentInChildren<AINavMovement>();
            navMovement.OnReachDestination.AddListener(OnReachDestination);
            myStats = GetComponent<UnitStats>();
        }

        public void UpdateStats(int dmg, float range, float ar)
        {
            attackDamage = dmg;
            attackRange = range;
            attackRate = ar;
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
                else
                {
                    if (!isAttackOnCooldown)
                        StartCoroutine(Attack());
                    
                    yield return new WaitForSeconds(0.05f);
                }

            }
        }

        IEnumerator Attack()
        {
            isAttackOnCooldown = true;
            Debug.Log("ATTACK!");

            yield return new WaitForSeconds(0.2f);

            if (projectilePrefab != null)
            {
                var position = transform.position;
                var projectile = Instantiate(projectilePrefab, position, quaternion.identity);
                Vector2 direction = (targetStats.transform.position - position).normalized;
                projectile.Setup(attackDamage, direction, true);
            }
            else
            {
                targetStats.OnTakeDamage(attackDamage);
                myStats.PlayItemAnimation();
            }

            yield return new WaitForSeconds(attackRate);
            isAttackOnCooldown = false;
        }
        

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
            Debug.Log("SET TARGET: " + stats);
            targetStats = stats;
            if(stats != null)
                StartCoroutine(AttackBehaviour());
        }

        
        
    }
}