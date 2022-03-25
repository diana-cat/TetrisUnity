using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private SpriteRenderer[,] sprites;

    void Start()
    {
        sprites = new SpriteRenderer[Grid.Width, Grid.Height];
        
        for (int i = 0; i < Grid.Width; i++)
        {
            for (int j = 0; j < Grid.Height; j++)
            {
                var instance = Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);
                sprites[i, j] = instance.GetComponent<SpriteRenderer>();
            }
        }
    }

    public void DrawCells(IEnumerable<Cell> cells)
    {
        foreach (var item in cells)
        {
            var x = item.Position.x;
            var y = item.Position.y;
            
            sprites[x, y].color = item.Color; 
        }
    }
}
