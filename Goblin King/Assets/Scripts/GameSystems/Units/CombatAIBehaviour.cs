using System.Collections;
using GameSystems.Items.SO;
using GameSystems.Units.AI;
using GameSystems.Units.Enemies;
using Specific_Items;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Units
{
    public class CombatAIBehaviour : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnTargetKilled = new();
        private UnitStats targetStats;

        private UnitStats myStats;
        private AINavMovement navMovement;

        private bool isInRangeOfTarget;
        private bool isAttackOnCooldown;

        private ItemSO_Weapon weapon;
        private bool isProjectileWeapon;
        
        private void Awake()
        {
            navMovement = GetComponentInChildren<AINavMovement>();
            myStats = GetComponent<UnitStats>();
        }

        public void UpdateStats(ItemSO_Weapon weaponSO)
        {
            weapon = weaponSO;
            isProjectileWeapon = weaponSO.projectile != null;
        }

        private IEnumerator AttackBehaviour()
        {
            while (targetStats != null && targetStats.hp > 0)
            {
                if (!IsInRangeOfTarget())
                {
                    navMovement.SetDestination(targetStats.transform.position, weapon.range);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    if (!isAttackOnCooldown)
                        StartCoroutine(Attack());
                    
                    yield return new WaitForSeconds(0.05f);
                }
            }

            OnTargetKilled.Invoke();
            targetStats = null;
        }

        private IEnumerator Attack()
        {
            isAttackOnCooldown = true;

            yield return new WaitForSeconds(0.2f);

            if (isProjectileWeapon)
            {
                var position = transform.position;
                var projectile = Instantiate(weapon.projectile, position, quaternion.identity).GetComponent<Projectile>();
                Vector2 direction = (targetStats.transform.position - position).normalized;
                projectile.Setup(weapon.damage, direction, true, myStats as EnemyStats);
            }
            else
            {
                targetStats.OnTakeDamage(weapon.damage, weapon.knockBack, transform.position);
                myStats.PlayItemAnimation();
            }

            yield return new WaitForSeconds(weapon.attackRate);
            isAttackOnCooldown = false;
        }
        

        bool IsInRangeOfTarget()
        {
            float distance = Vector2.Distance(transform.position, targetStats.transform.position);
            return distance <= weapon.range;
        }

        public void SetTarget(UnitStats stats)
        {
            targetStats = stats;
            if(stats != null)
                StartCoroutine(AttackBehaviour());
        }

        
        
    }
}