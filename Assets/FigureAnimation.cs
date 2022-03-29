using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureAnimation : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform figure;


    public void StartAnimation(Transform figure, float deathLine) 
    {
        this.figure= figure;
        StartCoroutine(Animation(deathLine));
    }


    private IEnumerator Animation(float deathLine)
    {
        while (figure.transform.position.y > deathLine)
        {
            figure.transform.position = new Vector3(figure.transform.position.x, figure.transform.position.y - speed * Time.deltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
    }
}
