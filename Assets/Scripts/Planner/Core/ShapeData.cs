using System;
using System.Collections.Generic;

[Serializable]
public class Shapes
{
    public List<Points> shapes = new List<Points>();
}

[Serializable]
public class Points
{
    public string name;
    public List<Vertex> vertices = new List<Vertex>();
}

[Serializable]
public class Vertex
{
    public string name;
    public float x;
    public float y;
    public float z;
}
