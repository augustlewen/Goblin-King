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

        [SerializeField] private ItemSO[] items;
        private ItemSO_Bag[] bags;
        private ItemSO_Tool[] tools;

        // private List<ItemToolData> toolDatas = new();
        // private List<ItemBagData> bagDatas = new();

        [FormerlySerializedAs("droppedItemPrefab")] public Loot lootPrefab;

        private void Awake()
        {
            i = this;
        }

        public void DropItems(List<ItemData> itemDatas, Vector2 position)
        {
            var droppedItem = Instantiate(lootPrefab, position, quaternion.identity).GetComponent<Loot>();
            droppedItem.SetLootTable(itemDatas);
        }
        //
        // // ReSharper disable Unity.PerformanceAnalysis
        // public static ItemData CreateItemData(ItemSO item)
        // {
        //     switch (item.itemType)
        //     {
        //         case ItemType.Bag : var bag = new ItemBagData(item as ItemSO_Bag);
        //             // i.bagDatas.Add(bag);
        //             return bag;
        //         case ItemType.Tool : var tool = new ItemToolData(item as ItemSO_Tool);
        //             // i.toolDatas.Add(tool);
        //             return tool;
        //         case ItemType.Resource : var resource = new ItemData(item);
        //             return resource;
        //     }
        //
        //     return null;
        // }

        // public System.Object GetItemSO(ItemData itemData)
        // {
        //     switch (itemData.itemSO.itemType)
        //     {
        //         case ItemType.Bag: return itemData.itemSO as ItemSO_Bag;
        //         case ItemType.Tool: return itemData.itemSO as ItemSO_Tool;
        //         case ItemType.Resource: return itemData.itemSO as ItemSO_Resource;
        //     }
        //
        //     return null;
        // }
        //
        // public System.Object GetSpecificItemData(ItemData itemData)
        // {
        //     switch (itemData.itemSO.itemType)
        //     {
        //         case ItemType.Bag: return itemData as ItemBagData;
        //         case ItemType.Tool: return itemData as ItemToolData;
        //         case ItemType.Resource: return itemData as ItemResourceData;
        //     }
        //
        //     return null;
        // }
        

        // /// <summary>
        // /// Either returns the correct ItemToolData or null if item is not a tool.
        // /// </summary>
        // public static ItemToolData GetToolData(ItemData item)=>
        //     item.GetItemType() != ItemType.Bag ? null : i.toolDatas[item.itemClassIndex];
        //
        // /// <summary>
        // /// Either returns the correct ItemBagData or null if item is not a bag.
        // /// </summary>
        // public static ItemBagData GetBagData(ItemData item) =>
        //     item.GetItemType() != ItemType.Bag ? null : i.bagDatas[item.itemClassIndex];
        //
    }
}