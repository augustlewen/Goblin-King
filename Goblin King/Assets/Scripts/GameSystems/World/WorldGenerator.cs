using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace GameSystems.World
{
    public class WorldGenerator : MonoBehaviour
    {
        public int chunkSize = 32;  // Size of each chunk
        public TileBase grassTile;  // Grass tile reference
        public TileBase waterTile;  // Water tile reference
        public TileBase dirtTile;   // Rock tile reference
        public TileBase rockTile;   // Rock tile reference
        public float scale = 0.1f;  // Scale for Perlin Noise

        public int seed;
        
        // Dictionary to store generated chunks
        private readonly Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();
        private System.Random random;  // A random generator instance based on the seed


        private void Start()
        {
            if (seed == 0)
                seed = Random.Range(0, int.MaxValue);

            
            random = new System.Random(seed);
        }


        // Method to get or create a chunk at specific coordinates
        public Chunk GetOrCreateChunk(Vector2Int chunkCoord, Tilemap tilemap)
        {
            if (!chunks.ContainsKey(chunkCoord))
            {
                // Create a new chunk if it doesn't exist
                Chunk newChunk = new Chunk(chunkCoord, chunkSize, tilemap, grassTile, waterTile, rockTile, dirtTile, scale, seed);
                newChunk.Generate();
                chunks[chunkCoord] = newChunk;
            }
            return chunks[chunkCoord];
        }

        // Method to unload a chunk
        public void UnloadChunk(Vector2Int chunkCoord)
        {
            if (chunks.ContainsKey(chunkCoord))
            {
                chunks[chunkCoord].ClearTiles();
                chunks.Remove(chunkCoord);
            }
        }
    }
}
