using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class Chunk
    {
        private Vector2Int chunkCoord;                  // Chunk's grid position
        private readonly int chunkSize;                 // Number of tiles in this chunk
        private readonly Tilemap tilemap;               // Reference to Tilemap
        private readonly TileBase grassTile, waterTile, 
            rockTile, dirtTile;                         // Tile types
        private readonly float scale;                   // Scale for Perlin Noise
        private readonly int seed;

        private readonly WorldGenerator worldGenerator;
        // Seed for random generation
        public Chunk(Vector2Int coord, Tilemap map, WorldGenerator wg)
        {
            chunkCoord = coord;
            tilemap = map;
            chunkSize = wg.chunkSize;
            grassTile = wg.grassTile;
            waterTile = wg.waterTile;
            rockTile = wg.rockTile;
            dirtTile = wg.dirtTile;
            scale = wg.scale;
            seed = wg.seed;
            worldGenerator = wg;
        }

        // Main Generate method
        public void Generate()
        {
            GenerateTerrain();
            GenerateLakes();  // Call the lake generation as a separate step
            worldGenerator.objectSpawner.SpawnObjects(chunkCoord, chunkSize);
        }

        // Generate basic terrain
        private void GenerateTerrain()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int worldX = chunkCoord.x * chunkSize + x;
                    int worldY = chunkCoord.y * chunkSize + y;

                    // float perlinValue = Mathf.PerlinNoise((worldX + seed) * scale, (worldY + seed) * scale);
                    TileBase selectedTile = grassTile; // Default to grass

                    // if (perlinValue > 0.75f)
                    //     selectedTile = rockTile;

                    tilemap.SetTile(new Vector3Int(worldX, worldY, 0), selectedTile);
                }
            }
        }

        // Generate lakes as a second pass
        private void GenerateLakes()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int worldX = chunkCoord.x * chunkSize + x;
                    int worldY = chunkCoord.y * chunkSize + y;

                    // Perlin noise for lake placement (independent of terrain generation)
                    float perlinValue = Mathf.PerlinNoise((worldX + seed) * scale, (worldY + seed) * scale);
                
                    // Lakes for low noise values
                    if (perlinValue < 0.3f)  // Adjust to control frequency of lakes
                    {
                        tilemap.SetTile(new Vector3Int(worldX, worldY, 0), waterTile);
                    }
                }
            }
        }

        // Clear tiles for unloading the chunk
        public void ClearTiles()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int worldX = chunkCoord.x * chunkSize + x;
                    int worldY = chunkCoord.y * chunkSize + y;
                    tilemap.SetTile(new Vector3Int(worldX, worldY, 0), null);
                }
            }
        }
    }
}