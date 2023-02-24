using System;
using System.Collections.Generic;

[Serializable]
public class Room
{
    public string name;
    public Position[] vertices =
    {
        new Position( 1, 1, 1),
        new Position(-1, 1, 1),
        new Position(-1,-1, 1),
        new Position( 1,-1, 1),
        new Position(-1, 1,-1),
        new Position( 1, 1,-1),
        new Position( 1,-1,-1),
        new Position(-1,-1,-1)
    };
    public List<Prop> props = new List<Prop>();
}

[Serializable]
public class Prop
{
    public int id;
    public Position position;

    public Prop(int id, float x, float y, float z)
    {
        this.id = id;
        position = new Position(x, y, z);
    }
}

[Serializable]
public class Position
{

    public float x;
    public float y; 
    public float z;

    public Position(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
