using GameSystems.Storage;
using Specific_Items;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class StorageObject : MonoBehaviour
    {
        private StorageData storage;

        public void Setup(int size)
        {
            storage = new StorageData(size);
        }
        
        
    }
}