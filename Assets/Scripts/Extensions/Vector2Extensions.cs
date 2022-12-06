using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 Abs(this Vector2 vector) =>
        new Vector2(vector.x.Abs(), vector.y.Abs());

    public static float Angle(this Vector2 from, Vector2 to) =>
        Vector2.Angle(from, to);

    public static Vector2 WithX(this Vector2 value, float x)
    {
        value.x = x;
        return value;
    }

    public static Vector2 WithY(this Vector2 value, float y)
    {
        value.y = y;
        return value;
    }

    public static Vector2 AddX(this Vector2 value, float x)
    {
        value.x += x;
        return value;
    }

    public static Vector2 AddY(this Vector2 value, float y)
    {
        value.y += y;
        return value;
    }
}