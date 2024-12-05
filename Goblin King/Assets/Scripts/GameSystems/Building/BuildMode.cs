using System;
using GameSystems.GridObjects;
using GameSystems.World.Grid;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Building
{
    public class BuildMode : MonoBehaviour
    {
        public static BuildMode i;
        
        private GridObjectSO buildGridObject;
        public PlacementObject placementObject;

        private bool isBuilding;
        public PlannedObject plannedObjectPrefab; 

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
            var plannedObject = Instantiate(plannedObjectPrefab, location, quaternion.identity);
            plannedObject.Setup(buildGridObject);
            ExitBuildMode();
        }
    }
}