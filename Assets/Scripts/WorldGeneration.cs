﻿using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class WorldGeneration : UdonSharpBehaviour
{
    [SerializeField] private double worldSeed;
    [SerializeField] private float interpolationWeight;
    [SerializeField] private GameObject redTile;
    [SerializeField] private GameObject greenTile;
    [SerializeField] private GameObject blueTile;
    [SerializeField] private Transform startingPoint;

    private double GenerateNumber(double x, double seed)
    {   
        // I came up with this by messing around in Desmos.
        double number = 3;
        number *= Math.Cos(x + seed);
        number *= Math.Sin(x - 1 + 1/seed);
        number *= Math.Cos(x + 20);
        number *= Math.Sin(x - 20);
        number *= Math.Sin(x + 4);
        return number;
    }

    private Vector2 UnitGradientVector(Vector2 gridPoint, double seed)
    {
        Vector2 gradientVector = new Vector2((float)GenerateNumber((double)gridPoint.x, seed),
                                             (float)GenerateNumber((double)gridPoint.y, seed));
        return gradientVector.normalized;
    }

    private float Interpolate(float value1, float value2, float weight)
    {
        return value1 + weight * (value2 - value1);
    }

    private float PerlinNoise(Vector2 point, float cell_size, int grid_width, int grid_height)
    {
        // Grid points
        Vector2 bottomLeftGridPoint = new Vector2((float)Math.Floor(point.x / cell_size) * cell_size, 
                                                  (float)Math.Floor(point.y / cell_size) * cell_size);
        Vector2 bottomRightGridPoint = new Vector2(bottomLeftGridPoint.x + cell_size, bottomLeftGridPoint.y);
        Vector2 topLeftGridPoint = new Vector2(bottomLeftGridPoint.x, bottomLeftGridPoint.y + cell_size);
        Vector2 topRightGridPoint = new Vector2(bottomLeftGridPoint.x + cell_size, bottomLeftGridPoint.y + cell_size);

        // Offset vectors
        Vector2 bottomLeftOffset = point - bottomLeftGridPoint;
        Vector2 bottomRightOffset = point - bottomRightGridPoint;
        Vector2 topLeftOffset = point - topLeftGridPoint;
        Vector2 topRightOffset = point - topRightGridPoint;

        // Unit gradient vectors
        Vector2 bottomLeftUnitGradient = UnitGradientVector(bottomLeftGridPoint, worldSeed);
        Vector2 bottomRightUnitGradient = UnitGradientVector(bottomRightGridPoint, worldSeed);
        Vector2 topLeftUnitGradient = UnitGradientVector(topLeftGridPoint, worldSeed);
        Vector2 topRightUnitGradient = UnitGradientVector(topRightGridPoint, worldSeed);

        // Dot products
        float bottomLeftDot = Vector2.Dot(bottomLeftUnitGradient, bottomLeftOffset);
        float bottomRightDot = Vector2.Dot(bottomRightUnitGradient, bottomRightOffset);
        float topLeftDot = Vector2.Dot(topLeftUnitGradient, topLeftOffset);
        float topRightDot = Vector2.Dot(topRightUnitGradient, topRightOffset);

        // Interpolate
        float firstInterpolation = Interpolate(bottomLeftDot, bottomRightDot, interpolationWeight);
        float secondInterpolation = Interpolate(topLeftDot, topRightDot, interpolationWeight);
        float finalInterpolation = Interpolate(firstInterpolation, secondInterpolation, interpolationWeight);

        return finalInterpolation;
    }

    private void Start()
    {
        for(int x = 0; x < 16; x++)
        {
            for(int y = 0; y < 16; y++)
            {
                GameObject tile;
                float noise = PerlinNoise(new Vector2(x, y), 1.2f, 20, 20);
                //Debug.Log(noise);
                if(noise > 1f)
                {
                    tile = blueTile;
                }
                else
                {
                    tile = greenTile;
                }
                Instantiate(tile, startingPoint.position + new Vector3(x, y, 0), startingPoint.rotation);
            }
        }
    }
}
