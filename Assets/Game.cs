using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    private Grid grid;
    private Figure figure;
    private Draw draw;
    private FigureCreator creator;


    void Start()
    {
        creator = new FigureCreator();

        figure = creator.CreateNewFigure();
        grid = GetComponent<Grid>();
        draw = GetComponent<Draw>();
    }


    void Update()
    {
        if (!figure.IsDead) 
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                figure.RotateLeft(grid);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                figure.MoveLeft(grid);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                figure.MoveRight(grid);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                figure.MoveDown(grid);
            }
        }
        else
        {
            grid.DiedFigure(figure.GetCells());
            figure = creator.CreateNewFigure();

            var count = grid.FindIndexRowsToRemove().Count;          
            if(count > 0)
            {
                grid.RemoveRow(grid.FindIndexRowsToRemove());
            }
        }
    }

    private void FixedUpdate()
    {
        draw.DrawCells(grid.GetCells());
        draw.DrawCells(grid.SkipBadPoint(figure.GetCells()));
    }
}
