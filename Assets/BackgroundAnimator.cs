using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    private const int FigureCount = 10;

    [SerializeField]
    private float deathLine;
    [SerializeField]
    [Range(0, 20)]
    private float offset;
    [SerializeField]
    private GameObject[] templates;

    private List<FigureAnimation> figures;


    private void Start()
    {
        figures = new List<FigureAnimation>();
        for (int i = 0; i < Mathf.Max(FigureCount, templates.Length); i++)
        {
            FigureAnimation newFigure = null;
            if (i < templates.Length)
            {
                newFigure = Instantiate(templates[i], new Vector3(-10, 0, 0), Quaternion.identity).GetComponent<FigureAnimation>();
            }
            else 
            {
                var index = Random.Range(0, templates.Length);
                newFigure = Instantiate(templates[index], new Vector3(-10, 0, 0), Quaternion.identity).GetComponent<FigureAnimation>();
            }

            figures.Add(newFigure);
        }

        StartCoroutine(Animation());
    }

    private FigureAnimation ChoiceFigure()
    {
        foreach (var item in figures)
        {
            if (item.IsDead) 
            {
                return item;
            }
        }

        return null;                
    }


    private IEnumerator Animation()
    {
        while (true) 
        {
            var activeFigure = ChoiceFigure();
           
            if (activeFigure == null) 
            {
                yield return new WaitForSeconds(1f);
                continue;
            }


            var pivot = this.transform.position;
            activeFigure.transform.position = new Vector3(Random.Range(pivot.x - offset, pivot.x + offset), pivot.y);            
            activeFigure.StartAnimation(deathLine);

            yield return new WaitForSeconds(1f);
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
}
