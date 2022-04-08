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
        grid = new Grid(); 
        draw = this.GetComponent<Draw>();

        figure = creator.CreateNewFigure();

        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        while (true)
        {
            if (!figure.IsDead)
            {
                figure.MoveDown(grid);
            }
            else
            {
                grid.DiedFigure(figure.GetCells());
                figure = creator.CreateNewFigure();

                var count = grid.FindIndexRowsToRemove().Count;
                if (count > 0)
                {
                    grid.RemoveRow(grid.FindIndexRowsToRemove());
                }
            }
            yield return new WaitForSeconds(0.4f);
        }       
    }

    void Update()
    {
        if (figure.IsDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            figure.RotateLeft(grid);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            figure.MoveToEnd(grid);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            figure.MoveLeft(grid);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            figure.MoveRight(grid);
        }
    }

    private void FixedUpdate()
    {
        draw.DrawCells(grid.GetCells());
        draw.DrawCells(grid.SkipBadPoint(figure.GetCells()));
    }
}
