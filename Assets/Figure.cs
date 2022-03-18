using UnityEngine;

public class Figure : MonoBehaviour
{   
    private Vector3Int pivot;
    [SerializeField]
    private Vector3Int[] points; // 4 points
    [SerializeField]
    private TypeFigure type;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            RotateLeft();        
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var item in points)
        {
            Gizmos.DrawCube(item, Vector3.one);
        }
    }

    public void MoveLeft()
    {
        pivot += Vector3Int.left;
    }

    public void MoveRight()
    {
        pivot += Vector3Int.right;
    }

    public void MoveDown()
    {
        pivot += Vector3Int.down;
    }

    public void RotateLeft()
    {
        if (type == TypeFigure.Matrix3x3)
        {
            for (int i = 0; i < points.Length; i++)
            {
                int x1 = points[i].x;
                points[i].x = points[i].y;
                points[i].y = - x1;
            }
        }
        else if (type == TypeFigure.Matrix4x4)
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        if(IsHorizontal())
        {
            int x = points[1].y;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].y = i;
                points[i].x = x;
            }
        }
        else
        {
            int y = points[1].x;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].x = i;
                points[i].y = y;
            }
        }
    }

    private bool IsHorizontal()
    {
        int count = 0;
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (points[i].y == points[i + 1].y)
            {
                count++;
            }
        }

        if (count == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}