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

        private void Awake()
        {
            gameObject.AddComponent<CombatAIBehaviour>();
        }

        public virtual void OnTakeDamage(int damage)
        {
            hp -= damage;
            
            if(hp <= 0)
                OnDeath();
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