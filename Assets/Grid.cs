using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Grid : MonoBehaviour
{
    private int width = 10;
    private int height = 20;
    private int[] cells;

    private void Start()
    {
        cells = new int[width * height];
    }

    public bool IsCellsEmpty(IEnumerable<Vector3Int> points)
    {
        foreach (var item in points)
        {
            if (Check(item))
            {
                return false;
            }
        }

        return true;
        
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Gizmos.DrawWireCube(new Vector3(i, j), Vector3.one * 0.9f);
            }
        }
    }

    private bool Check(Vector3Int point)
    {
        return (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height);
    }
}

enum TypeFigure
{
    Matrix2x2,
    Matrix3x3,
    Matrix4x4
}
