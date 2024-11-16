using System;
using GameSystems.Items.SO;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace GameSystems.Items
{
    public class DroppedItem : MonoBehaviour
    {
        public ItemSO item;

        private void Start()
        {
            ItemManager.CreateItemData(item);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GoblinStats goblinStats = other.GetComponent<GoblinStats>();

            if (goblinStats != null && goblinStats.bag != null)
            {
                if(goblinStats.bag.AddItem(item.GetItemData()))
                    Destroy(gameObject);
            }
        }
    }
}