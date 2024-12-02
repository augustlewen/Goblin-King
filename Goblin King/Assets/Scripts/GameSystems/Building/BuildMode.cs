using System;
using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.Building
{
    public class BuildMode : MonoBehaviour
    {
        public static BuildMode i;
        
        private GridObjectSO buildGridObject;
        public PlacementObject placementObject;

        private bool isBuilding;

        private void Awake()
        {
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
            ExitBuildMode();
        }
    }
}