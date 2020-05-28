using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Size")]

    [SerializeField]
    private int _size = 512;
    [SerializeField]
    private int _height = 256;

    private Terrain _terrain;
    private TerrainData _terrainData;

    [Header("Noise")]

    [SerializeField]
    private float _scale = 10f;

    [SerializeField]
    private float _offsetX = 0f;

    [SerializeField]
    private float _offsetZ = 0f;

    [Header("Splats")]
    [SerializeField]
    private SplatHeight[] _splatHeights;
    [SerializeField]
    private float _textureOrganicBlend;
    [SerializeField]
    private float _basePerlinMap;
    private void Awake()
    {
        _terrain = GetComponent<Terrain>();
        _terrainData = _terrain.terrainData;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }
    

    private void GenerateTerrain()
    {
        _terrainData.size = new Vector3(_size, _height, _size);
        _terrainData.heightmapResolution = _size + 1;
        _terrainData.SetAlphamaps(0, 0, GenerateSplatData());

        _terrainData.SetHeights(0, 0, GenerateHeights());
    }

    private float[,,] GenerateSplatData()
    {
        int mapSize = _terrainData.alphamapWidth;
        float[,,] splatMapData = new float[_size, _size, _terrainData.alphamapLayers];

        for (int z = 0; z < _terrainData.alphamapHeight; z++)
        {
            for (int x = 0; x < _terrainData.alphamapWidth; x++)
            {
                float terrainHeight = _terrainData.GetHeight(z, x);
                float[] splats = new float[_splatHeights.Length];

                for (int i = 0; i < _splatHeights.Length; i++)
                {
                    float curStartingHeight = _splatHeights[i].StartingHeight;// - _splatHeights[i].Overlap;

                    float nextStartingHeight = 0;

                    if (i != _splatHeights.Length - 1)
                    {
                        nextStartingHeight = _splatHeights[i + 1].StartingHeight;
                    }

                    float perlinNoise = Mathf.PerlinNoise(x * _textureOrganicBlend, z * _textureOrganicBlend);
                    perlinNoise = MapFloat(perlinNoise, 0f, 1f, _basePerlinMap, 1f);
                    curStartingHeight *= perlinNoise;
                    nextStartingHeight *= perlinNoise;

                    if (i == _splatHeights.Length - 1 && terrainHeight >= curStartingHeight)
                    {
                        splats[i] = 1;
                    }
                    else if (terrainHeight >= curStartingHeight && terrainHeight <= nextStartingHeight)
                    {
                        splats[i] = 1;
                    }
                }

                NormalizeArray(splats);

                for (int i = 0; i < _splatHeights.Length; i++)
                {
                    splatMapData[x, z, i] = splats[i];
                }
            }
        }

        return splatMapData;
    }

    private float MapFloat(float val, float min1, float max1, float min2, float max2)
    {
        return (val - min1) * (max2 - min2) / (max1 - min1) + min2;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[_size, _size];

        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                heights[z, x] = GetPerlinHeightAt(x, z);
            }
        }

        return heights;
    }

    private float GetPerlinHeightAt(float x, float z)
    {
        var xCoord = x / (float)_size * _scale + _offsetX;
        var zCoord = z / (float)_size * _scale + _offsetZ;

        return Mathf.PerlinNoise(xCoord, zCoord); ;
    }

    void NormalizeArray(float[] arr)
    {
        float total = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            total += arr[i];
        }

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] /= total;
        }
    }

    [System.Serializable]
    public class SplatHeight
    {
        public int StartingHeight;
        public int Overlap;
    }
}

