using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    
    private Grid grid;
    [SerializeField]
    private Figure[] figures;
    private Figure figure;


    void Start()
    {
        figure = figures[Random.Range(0, figures.Length)].Clone();
        grid = GetComponent<Grid>();
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
            figure = figures[Random.Range(0, figures.Length)].Clone();
            Debug.Log("No move");
        }
    }
}
