using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class PlayerChunkLoader : MonoBehaviour
    {
        public Transform player;               // Reference to player Transform
        public WorldGenerator worldGenerator;  // Reference to WorldGenerator
        public Tilemap tilemap;                // Reference to Tilemap for chunk display
        public int viewDistance = 2;           // Number of chunks to load around the player

        private Vector2Int currentChunkCoord;  // Track which chunk the player is in
        private HashSet<Vector2Int> activeChunks = new HashSet<Vector2Int>();

        void Start()
        {
            currentChunkCoord = GetChunkCoordFromPosition(player.position);
            UpdateChunks();
        }

        void Update()
        {
            Vector2Int newChunkCoord = GetChunkCoordFromPosition(player.position);
            if (newChunkCoord != currentChunkCoord)
            {
                currentChunkCoord = newChunkCoord;
                UpdateChunks();
            }
        }

        // Get chunk coordinates based on player world position
        private Vector2Int GetChunkCoordFromPosition(Vector3 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x / worldGenerator.chunkSize),
                Mathf.FloorToInt(position.y / worldGenerator.chunkSize)
            );
        }

        // Load new chunks around the player and unload distant ones
        private void UpdateChunks()
        {
            HashSet<Vector2Int> newActiveChunks = new HashSet<Vector2Int>();

            // Load and activate new chunks within view distance
            for (int x = -viewDistance; x <= viewDistance; x++)
            {
                for (int y = -viewDistance; y <= viewDistance; y++)
                {
                    Vector2Int chunkCoord = new Vector2Int(currentChunkCoord.x + x, currentChunkCoord.y + y);
                    newActiveChunks.Add(chunkCoord);

                    if (!activeChunks.Contains(chunkCoord))
                        worldGenerator.GetOrCreateChunk(chunkCoord, tilemap);
                }
            }

            // Deactivate chunks that are no longer within view distance
            foreach (var chunkCoord in activeChunks)
            {
                if (!newActiveChunks.Contains(chunkCoord))
                    worldGenerator.UnloadChunk(chunkCoord);
            }

            // Update activeChunks to the new set of active chunks
            activeChunks = newActiveChunks;
        }
    }
}