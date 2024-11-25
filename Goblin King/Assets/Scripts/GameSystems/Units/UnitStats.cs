using UnityEngine;

namespace GameSystems.Units
{
    public class UnitStats : MonoBehaviour
    {
        public int hp;
        public float moveSpeed;

        [HideInInspector] public float attackRange;
        [HideInInspector] public float attackDamage;
        [HideInInspector] public float attackRate;

        public virtual void OnTakeDamage(int damage)
        {
            hp -= damage;
            
            if(hp <= 0)
                OnDeath();
        }

        public virtual void OnDeath()
        {
        }
        
    }
}