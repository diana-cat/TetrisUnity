using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

[Serializable]
public class GameScoreChangeEvent : UnityEvent<int> { }

public class Game : MonoBehaviour
{
    private Grid grid;
    private Figure figure;
    private Draw draw;
    private FigureCreator creator;
    private int score;
    private int[] scoreFromRows = { 100, 400, 700, 1500 };
    [SerializeField]
    private UnityEvent gameEnd;
    [SerializeField]
    private UnityEvent gameStart;
    [SerializeField]
    private GameScoreChangeEvent scoreChange;


    void Start()
    {
        creator = new FigureCreator();
        grid = new Grid(); 
        draw = this.GetComponent<Draw>();

        Restart();
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
                try
                {
                    grid.DiedFigure(figure.GetCells());
                }
                catch
                {
                    gameEnd.Invoke();
                    yield break;
                }
                figure = creator.CreateNewFigure();

                var count = grid.FindIndexRowsToRemove().Count;
                if (count > 0)
                {
                    grid.RemoveRow(grid.FindIndexRowsToRemove());
                    score += scoreFromRows[count-1];
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

    public void Restart()
    {
        figure = creator.CreateNewFigure();
        grid.ClearGrid();
        score = 0;
        StartCoroutine(Fall());

        gameStart.Invoke();
    }
}
