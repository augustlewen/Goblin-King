using System;
using GameSystems.GridObjects;
using GameSystems.GridObjects.SO;
using GameSystems.World.Grid;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Building
{
    public class BuildMode : MonoBehaviour
    {
        public static BuildMode i;
        
        private GridObjectSO buildGridObject;
        public PlacementObject placementObject;

        private bool isBuilding;
        [FormerlySerializedAs("buildProjectObjectPrefab")] [FormerlySerializedAs("plannedObjectPrefab")] public BuildProject buildProjectPrefab; 

        private void Awake()
        {
            i = this;
            placementObject.OnSelectPlacement.AddListener(BuildAtLocation);
        }

        private void Update()
        {
            if(!isBuilding)   
                return;
            
            if(Input.GetMouseButtonDown(1))
                ExitBuildMode();
        }

        public void BeginBuildMode(GridObjectSO gridObjectSO)
        {
            buildGridObject = gridObjectSO;
            placementObject.SetObject(gridObjectSO);
            isBuilding = true;
        }

        private void ExitBuildMode()
        {
            buildGridObject = null;
            placementObject.SetObject(null);
            isBuilding = false;
        }

        private void BuildAtLocation(Vector2 location)
        {
            var plannedObject = Instantiate(buildProjectPrefab, location, quaternion.identity);
            plannedObject.Setup(buildGridObject);
            ExitBuildMode();
        }
    }
}