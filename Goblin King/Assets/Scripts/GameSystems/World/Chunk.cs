using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.World
{
    public class Chunk
    {
        private Vector2Int chunkCoord;      // Chunk's grid position
        private readonly int chunkSize;              // Number of tiles in this chunk
        private readonly Tilemap tilemap;            // Reference to Tilemap
        private readonly TileBase grassTile, waterTile, rockTile;  // Tile types
        private readonly float scale;                // Scale for Perlin Noise

        public Chunk(Vector2Int coord, int size, Tilemap map, TileBase grass, TileBase water, TileBase rock, float noiseScale)
        {
            chunkCoord = coord;
            chunkSize = size;
            tilemap = map;
            grassTile = grass;
            waterTile = water;
            rockTile = rock;
            scale = noiseScale;
        }

        public void Generate()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    // Calculate world position based on chunk position
                    int worldX = chunkCoord.x * chunkSize + x;
                    int worldY = chunkCoord.y * chunkSize + y;

                    // Generate Perlin noise value
                    float perlinValue = Mathf.PerlinNoise(worldX * scale, worldY * scale);

                    // Choose tile type based on noise value
                    TileBase selectedTile = grassTile;
                    if (perlinValue < 0.4f)
                        selectedTile = waterTile;
                    else if (perlinValue > 0.7f)
                        selectedTile = rockTile;

                    // Set tile in tilemap
                    tilemap.SetTile(new Vector3Int(worldX, worldY, 0), selectedTile);
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