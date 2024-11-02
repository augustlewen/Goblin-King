using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

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

        // Dictionary to store generated chunks
        private readonly Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

        // Method to get or create a chunk at specific coordinates
        public Chunk GetOrCreateChunk(Vector2Int chunkCoord, Tilemap tilemap)
        {
            if (!chunks.ContainsKey(chunkCoord))
            {
                // Create a new chunk if it doesn't exist
                Chunk newChunk = new Chunk(chunkCoord, chunkSize, tilemap, grassTile, waterTile, rockTile, dirtTile, scale);
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
