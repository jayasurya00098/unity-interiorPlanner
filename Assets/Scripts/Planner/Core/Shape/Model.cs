using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Model : Shape
{
    public List<Vector3> _vertices;
    public List<int> _triangles;

    //errorFix is bool used to avoid invert when the mesh is generated invertly
    public bool invert = false;

    public void GenerateCube()
    {
        _vertices = new List<Vector3>();
        _triangles= new List<int>();

        for (int i = 0; i < 6; i++)
        {
            GenerateFace(i);
        }
    }

    public void GenerateFace(int dir)
    {
        _vertices.AddRange(FaceVertices(dir));

        int count = _vertices.Count;

        _triangles.Add(count - 4);
        _triangles.Add(count - 4 + 1);
        _triangles.Add(count - 4 + 2);
        _triangles.Add(count - 4);
        _triangles.Add(count - 4 + 2);
        _triangles.Add(count - 4 + 3);
    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();

        //invert cube for inwardwalls (Remove this line to create cube)
        if (invert)
        {
            mesh.triangles = mesh.triangles.Reverse().ToArray();
        }

        mesh.RecalculateNormals();
    }
}
