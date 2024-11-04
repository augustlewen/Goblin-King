using System.Collections.Generic;
using System.Linq;
using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class WorldObjectSpawner : MonoBehaviour
    {
        public Tilemap tilemap;
        public ObjectSpawningData[] objectSpawningList;
        
        [System.Serializable]
        public class ObjectSpawningData
        {
            public GameObject prefab;
            public Transform parent;
            public Vector2Int spawnCountRange;
            public Vector2Int clusterCountRange;
            public SpawnBehaviour spawnBehaviour;
            
            public enum SpawnBehaviour
            {
                Normal,
                Branching,
                Cluster
            }
        }
        
        public void SpawnObjects(Vector2Int chunkCoord, int chunkSize, Transform chunk)
        {
            foreach (var objData in objectSpawningList)
            {
                switch (objData.spawnBehaviour)
                {
                    case ObjectSpawningData.SpawnBehaviour.Normal: 
                        for (int i = 0; i < Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y); i++)
                        {
                            Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
                            Instantiate(objData.prefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, chunk);
                        }
                        break;
                    case ObjectSpawningData.SpawnBehaviour.Cluster: GenerateCluster(chunkCoord, chunkSize, objData, chunk);
                        break;
                    case ObjectSpawningData.SpawnBehaviour.Branching: GenerateBranchingCluster(chunkCoord, chunkSize, objData, chunk);
                        break;
                }
            }
        }

        private Vector3Int GetRandomPosition(Vector2Int chunkCoord, int chunkSize)
        {
            int x = Random.Range(chunkCoord.x * chunkSize, (chunkCoord.x + 1) * chunkSize);
            int y = Random.Range(chunkCoord.y * chunkSize, (chunkCoord.y + 1) * chunkSize);
            return new Vector3Int(x, y, 0); // Assuming a 2D game
        }
        
        private Vector2Int GetRandomClusterCenter(Vector2Int chunkCoord, int chunkSize)
        {
            int centerX = Random.Range(0, chunkSize);
            int centerY = Random.Range(0, chunkSize);
            return new Vector2Int(chunkCoord.x * chunkSize + centerX, chunkCoord.y * chunkSize + centerY);
        }
        
        private void GenerateCluster(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData, Transform chunk)
        {
            // Loop through the number of clusters we want in this chunk
            for (int i = 0; i < Random.Range(objData.clusterCountRange.x, objData.clusterCountRange.y); i++)
            {
                Vector2Int clusterCenter = GetRandomClusterCenter(chunkCoord, chunkSize);
                int maxObjects = Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y);
                float clusterRadius = Random.Range(maxObjects * 0.03f + 2, maxObjects * 0.03f + 2);

                List<Vector2> viablePositions = new List<Vector2>();
                for (int j = 0; j < maxObjects * 2; j++) // Pre-generate more positions than needed
                {
                    Vector2 offset = Random.insideUnitCircle * clusterRadius;
                    Vector2 candidatePosition = new Vector2(
                        Mathf.RoundToInt((clusterCenter.x + offset.x) / WorldGrid.i.cellSize) * WorldGrid.i.cellSize,
                        Mathf.RoundToInt((clusterCenter.y + offset.y) / WorldGrid.i.cellSize) * WorldGrid.i.cellSize
                    );

                    if (IsValidPosition(candidatePosition) && !viablePositions.Contains(candidatePosition))
                        viablePositions.Add(candidatePosition);
                }

                viablePositions = viablePositions.Take(maxObjects).ToList();
                InstantiateObjectsAtPositions(viablePositions, objData, chunk);
            }
        }
        
        private void GenerateBranchingCluster(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData, Transform chunk)
        {
            // Loop through the number of clusters we want in this chunk
            for (int i = 0; i < Random.Range(objData.clusterCountRange.x, objData.clusterCountRange.y); i++)
            {
                int maxObjects = Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y);
                List<Vector2Int> clusterPositions = new List<Vector2Int>();
                HashSet<Vector2Int> visitedPositions = new HashSet<Vector2Int>();

                Vector2Int startPosition = GetRandomClusterCenter(chunkCoord, chunkSize);

                Queue<Vector2Int> positionsToCheck = new Queue<Vector2Int>();
                positionsToCheck.Enqueue(startPosition);
                visitedPositions.Add(startPosition);

                while (clusterPositions.Count < maxObjects && positionsToCheck.Count > 0)
                {
                    Vector2Int currentPos = positionsToCheck.Dequeue();
                    if (!WorldGrid.i.IsOccupied(currentPos))
                        clusterPositions.Add(currentPos);

                    // Get and shuffle neighbors
                    List<Vector2Int> neighbors = GetShuffledNeighbors(currentPos);

                    // Branching logic with randomness
                    foreach (var neighbor in neighbors)
                    {
                        if (visitedPositions.Contains(neighbor)) continue;

                        // Only proceed with a neighbor based on a branching probability
                        float branchChance = 0.6f; // Adjust for more or less density in branching
                        if (Random.value > branchChance) continue;
                        

                        // Add neighbor to queue and mark as visited
                        positionsToCheck.Enqueue(neighbor);
                        visitedPositions.Add(neighbor);
                    }
                }

                List<Vector2> spawnPositions = clusterPositions
                    .Select(p => new Vector2(p.x * WorldGrid.i.cellSize, p.y * WorldGrid.i.cellSize))
                    .ToList();

                InstantiateObjectsAtPositions(spawnPositions, objData, chunk);
            }
        }
        private void InstantiateObjectsAtPositions(List<Vector2> positions, ObjectSpawningData objData, Transform chunk)
        {
            foreach (var position in positions)
            {
                var obj = Instantiate(objData.prefab, position, Quaternion.identity, chunk);
                WorldGrid.i.AddObject(obj);  // Mark this position as occupied in the WorldGrid
            }
        }
        
        private List<Vector2Int> GetShuffledNeighbors(Vector2Int position)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>
            {
                position + Vector2Int.up,
                position + Vector2Int.down,
                position + Vector2Int.left,
                position + Vector2Int.right
            };
            return neighbors.OrderBy(n => Random.value).ToList(); // Shuffle neighbors
        }
        
        private bool IsValidPosition(Vector2 position)
        {
            return !WorldGrid.i.IsOccupied(position);
        }

        
    }
}