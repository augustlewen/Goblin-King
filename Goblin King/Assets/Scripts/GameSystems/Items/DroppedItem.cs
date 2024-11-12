using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace GameSystems.Items
{
    public class DroppedItem : MonoBehaviour
    {
        public ItemSO itemSO;
        
        private void OnTriggerEnter(Collider other)
        {
            GoblinStats goblinStats = other.GetComponent<GoblinStats>();

            if (goblinStats != null && goblinStats.bag != null)
            {
                goblinStats.bag.AddItem(itemSO);
            }
        }
    }
}