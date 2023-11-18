using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] TreePrefabs;
    [SerializeField]
    GameObject[] FellTreePrefabs;
    [SerializeField]
    GameObject[] SaplingPrefabs;
    [SerializeField]
    GameObject[] StonePrefabs;
    [SerializeField]
    GameObject[] GasCannister;
    [SerializeField]
    float amountOfTrees;
    [SerializeField]
    float amountOfFellTrees;
    [SerializeField]
    float amountOfSaplings;
    [SerializeField]
    float amountOfStones;
    [SerializeField]
    float amountOfGasCannister;

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

        SpawnObject(terrain, amountOfTrees, TreePrefabs);
        SpawnObject(terrain, amountOfFellTrees, FellTreePrefabs);
        SpawnObject(terrain, amountOfSaplings, SaplingPrefabs);
        SpawnObject(terrain, amountOfStones, StonePrefabs);
        SpawnObject(terrain, amountOfGasCannister, GasCannister);

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

    private void SpawnObject(Terrain terrain, float amount, GameObject[] gameObject)
    {
        for (int i = 0; i < amount; i++)
        {
            int model = Random.Range(0, gameObject.Length);
            // Generate random X and Z coordinates within a range
            float x = Random.Range(-128, 128);
            float z = Random.Range(-128, 128);
            
            // Use Terrain.SampleHeight to get the Y coordinate from the terrain
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            // Create a random spawn point at the terrain height
            Vector3 randomSpawnPoint = new Vector3(x, y, z);
            // Spawns object if only terrains colliders in found
            if (FindCollision(randomSpawnPoint) == 1)
            {
                Instantiate(gameObject[model], randomSpawnPoint, Quaternion.identity);
            }
        }
    }

    // Function to find the number of colliders within a spherical region at a given position
    private int FindCollision(Vector3 pos)
    {
        // Use Physics.OverlapSphere to find colliders within the specified radius at the given position
        // The result is an array of colliders that intersect with the sphere
        Collider[] hits = Physics.OverlapSphere(pos, 1f);
        // Return the number of colliders found in the specified layer
        return hits.Length;
    }

}
