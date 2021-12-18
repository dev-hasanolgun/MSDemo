using UnityEngine;

public struct Trace
{
    public Vector3 Position;
    public Quaternion Rotation;

    public Trace(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
