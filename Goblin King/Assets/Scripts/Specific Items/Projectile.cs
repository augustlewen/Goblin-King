using GameSystems.Items.SO;
using UnityEngine;

namespace Specific_Items
{
    public class Projectile : MonoBehaviour
    {
        public int speed;
        private int damage;
        
        public void Setup(int dmg, Vector2 direction, bool rotateTowardsDirection)
        {
            damage = dmg;
            var rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            
            rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
            
            if (rotateTowardsDirection)
            {
                float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angleZ);
            }
        }
    }
}