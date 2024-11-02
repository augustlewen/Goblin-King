using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class WorldObjectSpawner : MonoBehaviour
    {
        public Tilemap tilemap;         // Reference to your Tilemap
        public ObjectSpawningData[] objectSpawningList;
        
        [System.Serializable]
        public class ObjectSpawningData
        {
            public GameObject prefab;
            public Transform parent;
            public bool isCluster;
            public Vector2Int spawnCountRange;
            public Vector2Int clusterCountRange;

        }
        
        public void SpawnObjects(Vector2Int chunkCoord, int chunkSize)
        {
            foreach (var objData in objectSpawningList)
            {
                if (objData.isCluster)
                {
                    GenerateCluster(chunkCoord, chunkSize, objData);
                }
                else
                {
                    for (int i = 0; i < Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y); i++)
                    {
                        Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
                        Instantiate(objData.prefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, objData.parent);
                    }
                }
            }
            // for (int i = 0; i < numberOfTrees; i++)
            // {
            //     Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
            //     Instantiate(treePrefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, spawnParent);
            // }
            //
            // for (int i = 0; i < numberOfRocks; i++)
            // {
            //     Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
            //     Instantiate(stonePrefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, spawnParent);
            // }
            //
            // for (int i = 0; i < numberOfWaterBodies; i++)
            // {
            //     Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
            //     // Optionally check if the position is suitable for water (e.g., not overlapping with trees or rocks)
            //     Instantiate(waterPrefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, spawnParent);
            // }
        }

        private Vector3Int GetRandomPosition(Vector2Int chunkCoord, int chunkSize)
        {
            int x = Random.Range(chunkCoord.x * chunkSize, (chunkCoord.x + 1) * chunkSize);
            int y = Random.Range(chunkCoord.y * chunkSize, (chunkCoord.y + 1) * chunkSize);
            return new Vector3Int(x, y, 0); // Assuming a 2D game
        }
        
        private void GenerateCluster(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData)
        {
            // Loop through the number of clusters we want in this chunk
            for (int i = 0; i < Random.Range(objData.clusterCountRange.x, objData.clusterCountRange.y); i++)
            {
                // Randomly determine the center point for this cluster within the chunk
                int centerX = Random.Range(0, chunkSize);
                int centerY = Random.Range(0, chunkSize);
                Vector2Int clusterCenter = new Vector2Int(chunkCoord.x * chunkSize + centerX, chunkCoord.y * chunkSize + centerY);

                // Spawn rocks around the center point in a small radius
                int objects = Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y); // Number of rocks in this cluster
                float clusterRadius = Random.Range(2f, 5f); // Radius for the cluster

                for (int j = 0; j < objects; j++)
                {
                    // Generate a random position within the cluster radius
                    Vector2 offset = Random.insideUnitCircle * clusterRadius;
                    Vector2 spawnPosition = new Vector2(clusterCenter.x + offset.x, clusterCenter.y + offset.y);

                    // Instantiate the rock prefab at this position
                    Instantiate(objData.prefab, spawnPosition, Quaternion.identity, objData.parent);
                }
            }
        }
    }
}