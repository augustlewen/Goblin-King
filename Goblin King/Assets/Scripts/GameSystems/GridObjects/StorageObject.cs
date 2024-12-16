using GameSystems.GridObjects.SO;
using GameSystems.Storage;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class StorageObject : MonoBehaviour
    {
        private StorageData storage;

        public void Setup(GridObjectSO gridObjectSO)
        {
            var gosoStorage = gridObjectSO as GOSO_Storage;
            if(gosoStorage != null)
                storage = new StorageData(gosoStorage.size);
        }
        
        
    }
}