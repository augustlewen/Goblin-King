using System;
using GameSystems.Items.SO;
using GameSystems.Units;
using GameSystems.Units.Enemies;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace Specific_Items
{
    public class Projectile : MonoBehaviour
    {
        public int speed;
        private int damage;
        private float knockBackForce;

        private bool isTargetGoblin;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Setup(int dmg, Vector2 direction, bool rotateTowardsDirection, bool isTargetingGoblin)
        {
            isTargetGoblin = isTargetingGoblin;
            damage = dmg;
            
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            rb.AddForce(direction * speed, ForceMode.VelocityChange);
            
            if (rotateTowardsDirection)
            {
                float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angleZ);
            }
            
            Destroy(gameObject, 5);
        }


        private void OnTriggerEnter(Collider other)
        {
            var stats = other.GetComponent<UnitStats>();
            if(stats == null)
                return;
            
            if (isTargetGoblin && stats is EnemyStats || !isTargetGoblin && stats is GoblinStats)
                return; 
            
            stats.OnTakeDamage(damage, knockBackForce, transform.position);
            Destroy(gameObject);
        }
    }
}