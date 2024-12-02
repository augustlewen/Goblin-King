using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.Building
{
    public class BuildMenuUI : MonoBehaviour
    {
        public GridObjectSO[] buildableList;
        public BuildMenuObject buildMenuObjPrefab;
        public Transform layout;
        private void Start()
        {
            foreach (var goso in buildableList)
            {
                var buildMenuObject = Instantiate(buildMenuObjPrefab, layout);
                buildMenuObject.Setup(goso);
            }
        }
    }
}