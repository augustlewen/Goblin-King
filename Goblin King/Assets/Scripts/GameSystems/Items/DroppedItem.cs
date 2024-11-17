using System;
using GameSystems.Items.SO;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace GameSystems.Items
{
    public class DroppedItem : MonoBehaviour
    {
        public ItemSO item;
        public SpriteRenderer spriteRenderer;

        private void Start()
        {
            if(item != null)
                SetItem(item);
        }

        public void SetItem(ItemSO itemSO)
        {
            item = itemSO;
            spriteRenderer.sprite = item.sprite;
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