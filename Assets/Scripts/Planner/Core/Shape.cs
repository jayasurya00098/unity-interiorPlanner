using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeType type;

    [HideInInspector]
    public Mesh mesh;
    [HideInInspector]
    public MeshRenderer meshRenderer;
    
    /*
    [HideInInspector]
    public Vector3[] vertices = 
    {
        new Vector3( 1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(-1,-1, 1),
        new Vector3( 1,-1, 1),
        new Vector3(-1, 1,-1),
        new Vector3( 1, 1,-1),
        new Vector3( 1,-1,-1),
        new Vector3(-1,-1,-1)
    };*/

    [HideInInspector]
    public Vector3[] vertices =
    {
        new Vector3( 1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(-1,-1, 1),
        new Vector3( 1,-1, 1),
        new Vector3(-1, 1,-1),
        new Vector3( 1, 1,-1),
        new Vector3( 1,-1,-1),
        new Vector3(-1,-1,-1)
    };

    [HideInInspector]
    public int[][] faceTriangles =
    {
        new int[] { 0, 1, 2, 3},
        new int[] { 5, 0, 3, 6},
        new int[] { 4, 5, 6, 7},
        new int[] { 1, 4, 7, 2},
        new int[] { 5, 4, 1, 0},
        new int[] { 3, 2, 7, 6}
    };

    public Vector3[] FaceVertices(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]]; 
        }
        return fv;
    }
}

public enum ShapeType
{
    Line,
    Triangle,
    Plane,
    Cube,
    Sphere
}
