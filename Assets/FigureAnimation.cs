using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureAnimation : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float degreesPerSecond = 20;

    private Transform figureTransform;
    private Action[] action;

    public bool IsDead { get; private set; } = true;

    private void Awake()
    {
        figureTransform = this.GetComponent<Transform>();
        action = new Action[2]
        {
            Move,
            MoveAndRotate
        };
    }

    public void StartAnimation( float deathLine) 
    {
        IsDead = false;
        StartCoroutine(Animation(deathLine));
        
    }


    private IEnumerator Animation(float deathLine)
    {
        Action someAction = action[UnityEngine.Random.Range(0, action.Length)];
        while (figureTransform.position.y > deathLine)
        {
            someAction.Invoke();
            yield return new WaitForFixedUpdate();
        }
        IsDead = true;
    }

    private void Move()
    {
        var newPosition = figureTransform.position - Vector3.up * speed * Time.deltaTime;
        figureTransform.position = newPosition; 
    }

    private void MoveAndRotate()
    {
        Move();
        figureTransform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
    }
}
