using Specific_Items;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class StorageObject : MonoBehaviour
    {
        private Storage storage;

        public void Setup(int size)
        {
            storage = new Storage(size);
        }

        // public bool CanBePlacedInStorage(ItemData itemData)
        // {
        //     if (storageItems.Count < storageSize)
        //         return true;
        //
        //     return false;
        // }
        
        // public void AddToStorage(ItemData itemData)
        // {
        //     foreach (var storageItem in storageItems.Where(storageItem => storageItem.itemSO == itemData.itemSO))
        //     {
        //         itemData.itemCount = storageItem.AddToStack(itemData);
        //         if (itemData.itemCount == 0)
        //             break;
        //     }
        //
        //     if (itemData.itemCount > 0 && storageItems.Count < storageSize)
        //         storageItems.Add(itemData);
        // }
    }
}