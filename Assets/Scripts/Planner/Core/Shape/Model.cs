using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Model : Shape
{
    public List<Vector3> vertices = new List<Vector3>();

    public override void GenerateMesh(int shape, List<Vector3> vertices)
    {
        base.GenerateMesh(shape, vertices);
    }
}
