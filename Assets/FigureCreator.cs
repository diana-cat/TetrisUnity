using UnityEngine;

class FigureCreator
{
    private Figure[] figures;
    public FigureCreator()
    {
        figures = new Figure[] 
        {
            new Figure(new Vector3Int(4,20,0), //T
                       new Vector3Int[]
                       {
                             new Vector3Int(-1,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(0,-1,0),
                             new Vector3Int(1,-1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(172/255f, 73/255f, 245/255f)),

            new Figure(new Vector3Int(4,20,0), //L
                       new Vector3Int[]
                       {
                             new Vector3Int(0,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(0,1,0),
                             new Vector3Int(1,-1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(0, 255/255f, 0)),

            new Figure(new Vector3Int(4,20,0), //J
                       new Vector3Int[]
                       {
                             new Vector3Int(0,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(0,1,0),
                             new Vector3Int(-1,-1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(255/255f, 111/255f, 255/255f)),

            new Figure(new Vector3Int(4,20,0), //S
                       new Vector3Int[]
                       {
                             new Vector3Int(0,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(1,0,0),
                             new Vector3Int(-1,-1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(255/255f, 0, 0)),

            new Figure(new Vector3Int(4,20,0), //Z
                       new Vector3Int[]
                       {
                             new Vector3Int(0,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(-1,0,0),
                             new Vector3Int(1,-1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(255/255f, 127/255f, 0)),

            new Figure(new Vector3Int(4,20,0), //O
                       new Vector3Int[]
                       {
                             new Vector3Int(0,1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(1,0,0),
                             new Vector3Int(1,1,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(255/255f, 255/255f, 0)),

            new Figure(new Vector3Int(4,20,0), //I
                       new Vector3Int[]
                       {
                             new Vector3Int(0,-1,0),
                             new Vector3Int(0,0,0),
                             new Vector3Int(0,1,0),
                             new Vector3Int(0,2,0)
                       },
                       TypeFigure.Matrix3x3,
                       new Color(0, 255/255f, 255/255f)),
        };
    }

    public Figure CreateNewFigure()
    {
        return figures[Random.Range(0, figures.Length)].Clone();
    }
}
