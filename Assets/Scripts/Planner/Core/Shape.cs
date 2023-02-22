using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeType type;

    [HideInInspector]
    public Mesh mesh;
    [HideInInspector]
    public MeshRenderer meshRenderer;

    private int[] triangles = { 0, 1, 2 };

    public virtual void GenerateMesh(int shape, List<Vector3> _vertices)
    {
        Vector3[] vertices = new Vector3[_vertices.Count];

        for (int i = 0; i < _vertices.Count; i++)
        {
            vertices[i] = _vertices[i];
        }

        switch (shape)
        {
            case 0:
                mesh.vertices = vertices;
                break;
            case 1:
                mesh.vertices = vertices;
                mesh.triangles = triangles;
                break;
        }
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
