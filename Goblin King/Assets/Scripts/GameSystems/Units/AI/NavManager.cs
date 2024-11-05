using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.Units.AI
{
    public class NavManager : MonoBehaviour
    {
        private static NavManager i;
        private NavMeshSurface navMeshSurface;
        
        private void Awake()
        {
            i = this;
            navMeshSurface = GetComponent<NavMeshSurface>();
        }

        public static void Build()
        {
            if (IsSurfaceBuilt())
                return;
            
            i.navMeshSurface.BuildNavMesh();
        }

        public static void UpdateBounds(Vector2 chunkCoord, float chunkSize)
        {
            if (!IsSurfaceBuilt())
                return;
            
            // Calculate the bounds for this chunk
            Vector3 chunkCenterPosition = new Vector3(
                chunkCoord.x * chunkSize + chunkSize / 2,
                -1,
                -chunkCoord.y * chunkSize + chunkSize / 2
            );

            Debug.Log(chunkCenterPosition);
            Bounds chunkBounds = new Bounds(chunkCenterPosition, new Vector3(chunkSize, 2, chunkSize));
            i.UpdateBounds(chunkBounds);
        }

        private void UpdateBounds(Bounds chunkBounds)
        {
            // Update the NavMeshSurface bounds
            navMeshSurface.center = chunkBounds.center;
            navMeshSurface.size = chunkBounds.size;
            
            // Instead of building from scratch, update the area if needed
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);

            
            // Update only this chunk's area in the NavMesh
            // StartCoroutine(i.UpdateNavMeshInBounds(chunkBounds));
        }
        
        private IEnumerator UpdateNavMeshInBounds(Bounds updateBounds)
        {
            // Collect only the sources within the specified bounds
            List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();
            NavMeshBuilder.CollectSources(
                updateBounds,
                navMeshSurface.layerMask,
                navMeshSurface.useGeometry,
                navMeshSurface.defaultArea,
                new List<NavMeshBuildMarkup>(),
                sources
            );

            // Use surface settings and update only the relevant data within bounds
            var settings = navMeshSurface.GetBuildSettings();
            yield return NavMeshBuilder.UpdateNavMeshDataAsync(
                navMeshSurface.navMeshData,
                settings,
                sources,
                updateBounds
            );

            // Debug.Log("Updated NavMesh within specific bounds");
        }
        
        private static bool IsSurfaceBuilt()
        {
            return i.navMeshSurface.navMeshData != null;
        }
    }
}