using System;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.Units
{
    public class UnitStats : MonoBehaviour
    {
        public int hp;
        public float moveSpeed;
        public ItemData equippedItem;
        
        public SpriteRenderer itemSprite;
        
        [SerializeField] private Animator itemAnimator;

        public virtual void OnTakeDamage(int damage, float knockBackForce, Vector3 sourcePos)
        {
            hp -= damage;
            
            if(hp <= 0)
                OnDeath();
            else
            {
                if (knockBackForce > 0)
                {
                    Vector2 direction = (transform.position - sourcePos).normalized;
                    GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                }
            }
        }

        protected virtual void OnDeath()
        {
        }

        public void PlayItemAnimation()
        {
            itemAnimator.SetTrigger(Animator.StringToHash("PlayOnce"));
        }
        
    }
}