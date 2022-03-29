using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] templates;

    private GameObject[] figures;

    private GameObject figure;
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float deathLine;
    [SerializeField]
    [Range(0,20)]
    private float offset;



    private void Start()
    {
        figures = new GameObject[templates.Length];
        for (int i = 0; i < templates.Length; i++)
        {
            figures[i] = Instantiate(templates[i], new Vector3(-10, 0, 0), Quaternion.identity);
        }

        ChoiceFigure();
    }


    public void FixedUpdate()
    {
        if (figure != null)
        {
            if(figure.transform.position.y > deathLine)
            {
                figure.transform.position = new Vector3(figure.transform.position.x, figure.transform.position.y - speed * Time.deltaTime, 0);
                figure.transform.RotateAround(Vector3.forward, 5 * Time.deltaTime);
            }
            else
            {
                figure = null;
            }
        }
        else
        {
            ChoiceFigure();
        }
    }

    private void OnDrawGizmos()
    {
        var pivot = this.transform.position;

        var from = new Vector3(pivot.x - 10, deathLine);
        var to = new Vector3(pivot.x + 10, deathLine);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(from, to);


        Gizmos.color = Color.red;

        from = new Vector3(pivot.x - offset, pivot.y + offset);
        to = new Vector3(pivot.x - offset, pivot.y - offset);
        
        Gizmos.DrawLine(from, to);


        from = new Vector3(pivot.x + offset, pivot.y + offset);
        to = new Vector3(pivot.x + offset, pivot.y - offset);

        Gizmos.DrawLine(from, to);
    }

    private void ChoiceFigure()
    {
        figure = figures[Random.Range(0, figures.Length)];

        var pivot = this.transform.position;

        figure.transform.position = new Vector3(Random.Range(pivot.x - offset, pivot.x + offset), pivot.y);
    }
}
