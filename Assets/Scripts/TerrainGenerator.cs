using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BRACKEYS PROCEDRUAL TERRAIN

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public int scale = 10;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public bool randomOnStart = false;
    Terrain terrain;

    private void Start() {
        terrain = GetComponent<Terrain>();
        if(randomOnStart){
            offsetX = UnityEngine.Random.Range(0,999f);
            offsetY = UnityEngine.Random.Range(0,999f);
        }   
    }

    private void Update() {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0,0,GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                heights[x,y] = CalculateHeight(x,y);
            }
        }
        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord,yCoord);
    }
}
