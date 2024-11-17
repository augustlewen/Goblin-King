using System;
using System.Collections.Generic;
using GameSystems.Items.SO;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Items
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager i;

        [SerializeField] private ItemSO[] items;
        private ItemSO_Bag[] bags;
        private ItemSO_Tool[] tools;

        private List<ItemToolData> toolDatas = new();
        private List<ItemBagData> bagDatas = new();

        public DroppedItem droppedItemPrefab;

        private void Awake()
        {
            i = this;
        }

        public void DropNewItem(ItemSO itemSO, Vector2 position)
        {
            var droppedItem = Instantiate(droppedItemPrefab, position, quaternion.identity).GetComponent<DroppedItem>();
            droppedItem.SetItem(itemSO);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static ItemData CreateItemData(ItemSO item)
        {
            switch (item.itemType)
            {
                case ItemType.Bag : var bag = new ItemBagData(item as ItemSO_Bag);
                    i.bagDatas.Add(bag);
                    return bag;
                case ItemType.Tool : var tool = new ItemToolData(item as ItemSO_Tool);
                    i.toolDatas.Add(tool);
                    return tool;
            }

            return null;
        }

        /// <summary>
        /// Either returns the correct ItemToolData or null if item is not a tool.
        /// </summary>
        public static ItemToolData GetToolData(ItemData item)=>
            item.GetItemType() != ItemType.Bag ? null : i.toolDatas[item.itemClassIndex];
        
        /// <summary>
        /// Either returns the correct ItemBagData or null if item is not a bag.
        /// </summary>
        public static ItemBagData GetBagData(ItemData item) =>
            item.GetItemType() != ItemType.Bag ? null : i.bagDatas[item.itemClassIndex];
        
    }
}