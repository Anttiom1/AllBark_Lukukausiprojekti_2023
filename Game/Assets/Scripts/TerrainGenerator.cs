using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] myObjects;
    private int width = 256;
    private int height = 256;
    private int depth = 5;
    private float scale = 5f;
    private float offsetX = 100f;
    private float offsetY = 100f;

    void Start()
    {
        // Initialize the offset values with random values for procedural generation
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        // Get the Terrain component attached to this GameObject
        Terrain terrain = GetComponent<Terrain>();
        // Generate and set the terrain data
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        // ObjectSpawner
        {
            // Spawning trees
            int amountOfTrees = 0;
            while (amountOfTrees < 100)
            {
                // Select a random object from the myObjects array
                int randomIndex = Random.Range(0, myObjects.Length);

                // Generate random X and Z coordinates within a range
                float x = Random.Range(-128, 128);
                float z = Random.Range(-128, 128);

                // Use Terrain.SampleHeight to get the Y coordinate from the terrain
                float y = terrain.SampleHeight(new Vector3(x, 0, z));

                // Create a random spawn point with a slight height offset
                Vector3 randomSpawnPoint = new Vector3(x, y + 2, z);

                // Instantiate the selected object at the random spawn point
                Instantiate(myObjects[0], randomSpawnPoint, Quaternion.identity);

                amountOfTrees++;
            }

            // Spawning stones
            int amountOfStones = 0;
            while (amountOfStones < 50)
            {
                // Select a random object from the myObjects array
                int randomIndex = Random.Range(0, myObjects.Length);

                // Generate random X and Z coordinates within a range
                float x = Random.Range(-128, 128);
                float z = Random.Range(-128, 128);

                // Use Terrain.SampleHeight to get the Y coordinate from the terrain
                float y = terrain.SampleHeight(new Vector3(x, 0, z));

                // Create a random spawn point at the terrain height
                Vector3 randomSpawnPoint = new Vector3(x, y, z);

                // Instantiate the selected object at the random spawn point
                Instantiate(myObjects[1], randomSpawnPoint, Quaternion.identity);

                amountOfStones++;
            }
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Set the heightmap resolution of the terrain
        terrainData.heightmapResolution = width + 1;

        // Set the size of the terrain
        terrainData.size = new Vector3(width, depth, height);

        // Generate and set the heights for the terrain
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the height based on Perlin noise
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        // Calculate the height using Perlin noise with offset values
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
