using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Functions;
using GameSystems.GridObjects;
using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class WorldObjectSpawner : MonoBehaviour
    {
        public Tilemap tilemap;
        public ObjectSpawningData[] objectSpawningList;

        public HashSet<GameObject> breakableObjects;
        public HashSet<GameObject> nonBreakableObject;
        
        
        [System.Serializable]
        public class ObjectSpawningData
        {
            public GridObjectSO gridObjectSO;
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
                        StartCoroutine(SpawnNormalObjects(chunkCoord, chunkSize, objData, chunk));
                        break;
                    case ObjectSpawningData.SpawnBehaviour.Cluster:
                        StartCoroutine(GenerateCluster(chunkCoord, chunkSize, objData, chunk));
                        break;
                    case ObjectSpawningData.SpawnBehaviour.Branching:
                        StartCoroutine(GenerateBranchingCluster(chunkCoord, chunkSize, objData, chunk));
                        break;
                }
            }
        }
        
        private IEnumerator SpawnNormalObjects(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData, Transform chunk)
        {
            // Determine the number of objects to spawn within this chunk based on the spawn count range
            int spawnCount = Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y);
    
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3Int spawnPosition = GetRandomPosition(chunkCoord, chunkSize);
                // Instantiate(objData.prefab, tilemap.GetCellCenterWorld(spawnPosition), Quaternion.identity, chunk);
                // ObjectPooling.ActivateObject(breakableObjects, tilemap.GetCellCenterWorld(spawnPosition));
                ActivateObjects(tilemap.GetCellCenterWorld(spawnPosition), objData, chunk);

                // Optional: Yield after each object to spread out load across frames
                yield return null;
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
        
        private IEnumerator GenerateCluster(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData, Transform chunk)
        {
            for (int i = 0; i < Random.Range(objData.clusterCountRange.x, objData.clusterCountRange.y); i++)
            {
                Vector2Int clusterCenter = GetRandomClusterCenter(chunkCoord, chunkSize);
                int maxObjects = Random.Range(objData.spawnCountRange.x, objData.spawnCountRange.y);
                float clusterRadius = Random.Range(maxObjects * 0.03f + 2, maxObjects * 0.03f + 2);

                List<Vector2> viablePositions = new List<Vector2>();
                for (int j = 0; j < maxObjects * 2; j++)
                {
                    Vector2 offset = Random.insideUnitCircle * clusterRadius;
                    Vector2 candidatePosition = new Vector2(
                        Mathf.RoundToInt((clusterCenter.x + offset.x) / WorldGrid.i.cellSize) * WorldGrid.i.cellSize,
                        Mathf.RoundToInt((clusterCenter.y + offset.y) / WorldGrid.i.cellSize) * WorldGrid.i.cellSize
                    );

                    if (IsValidPosition(candidatePosition) && !viablePositions.Contains(candidatePosition))
                        viablePositions.Add(candidatePosition);
                }

                // Limit the viable positions to the desired maxObjects and spawn
                viablePositions = viablePositions.Take(maxObjects).ToList();
                
                foreach (var pos in viablePositions)
                {
                    ActivateObjects(pos, objData, chunk);

                    // Optional: yield after each spawn to distribute work
                    yield return null;
                }
            }
        }
        
        private IEnumerator GenerateBranchingCluster(Vector2Int chunkCoord, int chunkSize, ObjectSpawningData objData, Transform chunk)
        {
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

                    List<Vector2Int> neighbors = GetShuffledNeighbors(currentPos);

                    foreach (var neighbor in neighbors)
                    {
                        if (visitedPositions.Contains(neighbor)) continue;
                        float branchChance = 0.6f;
                        if (Random.value > branchChance) continue;

                        positionsToCheck.Enqueue(neighbor);
                        visitedPositions.Add(neighbor);
                    }

                    // Yield every few positions to control frame load
                    if (clusterPositions.Count % 10 == 0)
                    {
                        yield return null;
                    }
                }

                // Instantiate objects over time at cluster positions
                foreach (var pos in clusterPositions)
                {
                    Vector2 spawnPos = new Vector2(pos.x * WorldGrid.i.cellSize, pos.y * WorldGrid.i.cellSize);
                    // var obj = Instantiate(objData.prefab, spawnPos, Quaternion.identity, chunk);
                    // WorldGrid.i.AddObject(obj);
                    ActivateObjects(spawnPos, objData, chunk);

                    yield return null; // Yield after each spawn for smoothness
                }
            }
        }
        
        private void ActivateObjects(Vector2 position, ObjectSpawningData objData, Transform chunk)
        {
            // var obj = Instantiate(objData.prefab, position, Quaternion.identity, chunk);

            HashSet<GameObject> objects = nonBreakableObject;
            switch (objData.gridObjectSO.type)
            {
                case GridObjectType.Breakable : objects = breakableObjects;
                    break;
            }
            
            var obj = ObjectPooling.ActivateObject(objects ,position);
            obj.transform.parent = chunk;
            
            WorldGrid.i.AddObject(obj);  // Mark this position as occupied in the WorldGrid
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