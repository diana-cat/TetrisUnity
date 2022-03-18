using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    private float fallTime = 0.8f;

    public GameObject field;
    public GameObject pivot;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3Int(-1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3Int(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3Int(1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3Int(1, 0, 0);
            }
        }

        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime) ||
            Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3Int(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3Int(0, -1, 0);
            }
            previousTime = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 90);
            if (!ValidMove())
            {
                transform.RotateAround(pivot.transform.position, Vector3.forward, -90);
            }
        }

    }

    private bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 1 || roundedX > field.transform.localScale.x || roundedY < 0 || roundedY > field.transform.localScale.y)
            {
                return false;
            }
        }

        return true;
    }
}
