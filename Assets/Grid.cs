using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Grid
{
    public const int Width = 10;
    public const int Height = 20;
    private GridCell[,] cells;
    private Color color;

    public  Grid()
    {
        cells = new GridCell[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                cells[i, j].Color = Color.gray;
            }
        }
    }

    public IEnumerable<Cell> SkipBadPoint(IEnumerable<Cell> source) 
    {
        foreach (var item in source)
        {
            if (item.Position.x  < Width && item.Position.y < Height)
            {
                yield return item;
            }   
        }
    }

    public bool IsCellsEmpty(IEnumerable<Vector3Int> points)
    {
        foreach (var item in points)
        {
            if (IsOutside(item) || IsNotEmpty(item))
            {                
               return false;
            }
        }

        return true;        
    }

    private bool IsOutside(Vector3Int point)
    {
        return (point.x < 0 || point.x >= Width || point.y < 0);
    }

    private bool IsNotEmpty(Vector3Int point)
    {
        return (point.y < Height && cells[point.x, point.y].IsNotEmpty);
    }

    public void DiedFigure(IEnumerable<Cell> points)
    {
        foreach (var item in points)
        {
            cells[item.Position.x, item.Position.y].IsNotEmpty = true;
            cells[item.Position.x, item.Position.y].Color = item.Color;
        }
    }

    public List<int> FindIndexRowsToRemove()
    {
        List<int> indexRow = new List<int>();
        int tmp = 0;

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (cells[j, i].IsNotEmpty)
                {
                    tmp++;
                }
            }
            if (tmp == Width)
            {
                indexRow.Add(i);
            }
            tmp = 0;
        }

        return indexRow;
    }

    public void RemoveRow(List<int> rowsToRemove)
    {
        foreach (var i in rowsToRemove)
        {
            for (int j = 0; j < Width; j++)
            {
                cells[j, i].IsNotEmpty = !cells[j, i].IsNotEmpty;
                cells[j, i].Color = color;
            }
        }
        MoveAllDown(rowsToRemove);
    }

    private void MoveAllDown(List<int> rowsToRemove)
    {
        for (int i = 0; i < Height; i++)
        {
            int rowsUnder = rowsToRemove.Where(item => i > item).Count();
            for (int j = 0; j < Width; j++)
            {
                cells[j, i - rowsUnder] = cells[j, i];
            }
        }
    }

    public IEnumerable<Cell> GetCells()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                yield return new Cell()
                {
                    Position = new Vector3Int(i, j, 0),
                    Color = cells[i, j].Color
                };
            }
        }
    }

    public struct GridCell
    {
        public bool IsNotEmpty;
        public Color Color;
    }
}

