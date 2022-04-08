using System.Collections.Generic;
using UnityEngine;

public class Figure
{
    private Vector3Int pivot;
    private Vector3Int[] points; // 4 points
    private TypeFigure type; 
    private Color color;

    private bool isDead = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    public Figure(Vector3Int pivot, Vector3Int[] points,  TypeFigure type, Color color)
    {
        this.pivot = pivot;
        this.points = new Vector3Int[4];
        this.type = type;
        this.color = color;

        for (int i = 0; i < this.points.Length; i++)
        {
            this.points[i] = points[i];
        }
    }

    public IEnumerable<Vector3Int> PointsPivot()
    {
        foreach (var item in points)
        {
            yield return item + pivot;
        }
    }

    public void MoveToEnd(Grid grid)
    {
        while(!IsDead)
        {
            MoveDown(grid);
        }
    }

    public void MoveLeft(Grid grid)
    {
        pivot += Vector3Int.left;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.right;
        }
    }

    public void MoveRight(Grid grid)
    {
        pivot += Vector3Int.right;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.left;
        }
    }

    public void MoveDown(Grid grid)
    {
        pivot += Vector3Int.down;
        if (!grid.IsCellsEmpty(PointsPivot()))
        {
            pivot += Vector3Int.up;
            isDead = true;
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

    public void RotateLeft(Grid grid)
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


    public Figure Clone()
    {
        return new Figure(pivot, points, type, color);
    }

    public IEnumerable<Cell> GetCells()
    {
        foreach (var item in PointsPivot())
        {
            yield return new Cell()
            {
                Position = item,
                Color = color
            };
        }
    }
}

public enum TypeFigure
{
    Matrix2x2,
    Matrix3x3,
    Matrix4x4
}

public struct Cell 
{
    public Vector3Int Position;
    public Color Color;
}
