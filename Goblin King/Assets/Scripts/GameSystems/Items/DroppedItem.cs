using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace GameSystems.Items
{
    public class DroppedItem : MonoBehaviour
    {
        public ItemSO itemSO;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            GoblinStats goblinStats = other.GetComponent<GoblinStats>();

            if (goblinStats != null && goblinStats.bag != null)
            {
                if(goblinStats.bag.AddItem(itemSO))
                    Destroy(gameObject);
            }
        }
    }
}