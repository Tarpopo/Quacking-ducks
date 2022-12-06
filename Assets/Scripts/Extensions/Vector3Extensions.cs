using System;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 WithX(this Vector3 value, float x)
    {
        value.x = x;
        return value;
    }

    public static Vector3 WithY(this Vector3 value, float y)
    {
        value.y = y;
        return value;
    }

    public static Vector3 WithZ(this Vector3 value, float z)
    {
        value.z = z;
        return value;
    }

    public static Vector3 AddX(this Vector3 value, float x)
    {
        value.x += x;
        return value;
    }

    public static Vector3 AddY(this Vector3 value, float y)
    {
        value.y += y;
        return value;
    }

    public static Vector3 AddZ(this Vector3 value, float z)
    {
        value.z += z;
        return value;
    }

    public static Vector3 AddAxis(this Vector3 vector, Axis axis, float value)
    {
        _ = axis switch
        {
            Axis.X => vector.x = value,
            Axis.Y => vector.y = value,
            Axis.Z => vector.z = value,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
        return vector;
    }

    public static Vector3 AxisVector(this Axis axis, Vector3 vector, float value)
    {
        _ = axis switch
        {
            Axis.X => vector.x = value,
            Axis.Y => vector.y = value,
            Axis.Z => vector.z = value,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
        return vector;
    }

    public static float GetAxisValue(this Axis axis, Vector3 vector)
    {
        return axis switch
        {
            Axis.X => vector.x,
            Axis.Y => vector.y,
            Axis.Z => vector.z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
    }

    public static Vector2 To2(this Vector3 value) =>
        value;

    public static Vector3 ToPlaneX(this Vector3 value) =>
        new Vector3(0, value.y, value.z);

    public static Vector3 ToPlaneY(this Vector3 value) =>
        new Vector3(value.x, 0, value.z);

    public static Vector3 ToPlaneZ(this Vector3 value) =>
        new Vector3(value.x, value.y, 0);

    public static float Angle(this Vector3 from, Vector3 to) =>
        Vector3.Angle(from, to);

    public static Vector3 Project(this Vector3 vector, Vector3 normal) =>
        Vector3.Project(vector, normal);

    public static Vector3 Abs(this Vector3 vector) =>
        new Vector3(vector.x.Abs(), vector.y.Abs(), vector.z.Abs());

    public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 normal) =>
        Vector3.ProjectOnPlane(vector, normal);

    public static float GetLength(this Vector3[] path)
    {
        float length = 0f;
        for (int i = 1; i < path.Length; i++)
            length += Vector3.Distance(path[i - 1], path[i]);
        return length;
    }
}