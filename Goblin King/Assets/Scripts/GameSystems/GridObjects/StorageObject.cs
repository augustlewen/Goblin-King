using GameSystems.GridObjects.SO;
using GameSystems.Interactions;
using GameSystems.Storage;
using UI.General;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class StorageObject : MonoBehaviour, ISelect
    {
        [HideInInspector] public StorageData storage;

        private void Awake()
        {
            gameObject.AddComponent<MouseInteractable>();
        }
        
        public void Setup(GridObjectSO gridObjectSO)
        {
            var gosoStorage = gridObjectSO as GOSO_Storage;
            if(gosoStorage != null)
                StorageManager.AddStorage(gosoStorage.size);
            //     storage = new StorageData(gosoStorage.size);

        }
        
        public void SelectObject()
        {
            WindowManager.i.OpenStorageUI();
        }
        
        
    }
}