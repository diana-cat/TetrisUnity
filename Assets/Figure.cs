﻿using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField]
    private Vector3Int pivot;
    [SerializeField]
    private Vector3Int[] points; // 4 points
    [SerializeField]
    private TypeFigure type;
    [SerializeField]
    private Grid grid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            RotateLeft();        
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    private IEnumerable<Vector3Int> PointsPivot()
    {
        foreach (var item in points)
        {
            yield return item + pivot;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var item in PointsPivot())
        {
            Gizmos.DrawCube(item, Vector3.one);
        }
    }

    public void MoveLeft()
    {
        pivot += Vector3Int.left;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.right;
        }
    }

    public void MoveRight()
    {
        pivot += Vector3Int.right;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.left;
        }
    }

    public void MoveDown()
    {
        pivot += Vector3Int.down;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.up;
        }
    }

    public void RotateLeftCount()
    {
        if (type == TypeFigure.Matrix3x3)
        {
            for (int i = 0; i < points.Length; i++)
            {
                int x1 = points[i].x;
                points[i].x = points[i].y;
                points[i].y = - x1;
            }
        }
        else if (type == TypeFigure.Matrix4x4)
        {
            ChangePosition();
        }
    }

    public void RotateRightCount()
    {
        if (type == TypeFigure.Matrix3x3)
        {
            for (int i = 0; i < points.Length; i++)
            {
                int x1 = points[i].x;
                points[i].x = -points[i].y;
                points[i].y = x1;
            }
        }
        else if (type == TypeFigure.Matrix4x4)
        {
            ChangePosition();
        }
    }

    public void RotateLeft()
    {
        RotateLeftCount();
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            RotateRightCount();
        }
    }

    private void ChangePosition()
    {
        if(IsHorizontal())
        {
            int x = points[1].y;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].y = i;
                points[i].x = x;
            }
        }
        else
        {
            int y = points[1].x;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].x = i;
                points[i].y = y;
            }
        }
    }

    private bool IsHorizontal()
    {
        int count = 0;
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (points[i].y == points[i + 1].y)
            {
                count++;
            }
        }

        if (count == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}