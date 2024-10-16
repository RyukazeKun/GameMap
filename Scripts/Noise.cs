using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offsett)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++){
            float offsetX = prng.Next(-100000, 100000) + offsett.x;
            float offsetY = prng.Next(-100000, 100000) - offsett.y;
            octaveOffsets [i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }


        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        float minValue = float.MinValue;
        float maxValue = float.MaxValue;


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;


                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x * frequency;
                    float sampleY = (y - halfHeight)/ scale * frequency + octaveOffsets[i].y * frequency;

                    float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > minValue)
                {
                    minValue = noiseHeight;
                } else if (noiseHeight < maxValue)
                {
                    maxValue = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x,y] = Mathf.InverseLerp (minValue, maxValue, noiseMap[x,y]);
            }
        }

                return noiseMap;
    }

    internal static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float noiseScale, int octaves, float persistance, float lacunarity, object offset)
    {
        throw new NotImplementedException();
    }
}
