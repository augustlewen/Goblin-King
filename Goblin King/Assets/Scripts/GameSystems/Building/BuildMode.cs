using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.Building
{
    public class BuildMode : MonoBehaviour
    {
        private GridObjectSO buildGridObject;
        public PlacementObject placementObject;

        public void BeginBuildMode(GridObjectSO gridObjectSO)
        {
            buildGridObject = gridObjectSO;
            placementObject.SetObject(gridObjectSO);
        }

        private void ExitBuildMode()
        {
            buildGridObject = null;
            placementObject.SetObject(null);
        }

        public void BuildAtLocation(Vector2 location)
        {
            
        }
    }
}