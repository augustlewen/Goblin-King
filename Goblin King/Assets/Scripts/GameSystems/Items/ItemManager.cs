using System;
using System.Collections.Generic;
using GameSystems.Items.SO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace GameSystems.Items
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager i;

        public Loot lootPrefab;

        private void Awake()
        {
            i = this;
        }

        public void DropItems(List<ItemData> itemDatas, Vector2 position)
        {
            var droppedItem = Instantiate(lootPrefab, position, quaternion.identity).GetComponent<Loot>();
            droppedItem.SetLootTable(itemDatas);
            Debug.Log("SET ITEMS TO LOOT");
        }
    }
}