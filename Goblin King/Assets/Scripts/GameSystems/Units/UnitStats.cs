using System;
using GameSystems.Items.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Units
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private int maxHp;
        [HideInInspector] public int hp;
        public float moveSpeed;
        public ItemData equippedItem;
        
        public SpriteRenderer itemSprite;
        
        [SerializeField] private Animator itemAnimator;


        private void OnEnable()
        {
            hp = maxHp;
        }

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
            gameObject.SetActive(false);
        }

        public void PlayItemAnimation()
        {
            itemAnimator.SetTrigger(Animator.StringToHash("PlayOnce"));
        }
        
    }
}