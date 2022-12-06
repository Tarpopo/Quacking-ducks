using UnityEngine;

public static class Rigidbody2DExtensions
{
    public static float AddExplosionForce(this Rigidbody2D rigidbody, float damage, float force, float radius,
        Vector2 point, ForceMode2D mode = ForceMode2D.Force)
    {
        var direction = rigidbody.position - point;
        var distance = direction.magnitude;
        direction.Normalize();
        var koof = Mathf.InverseLerp(0, radius, distance);
        rigidbody.AddForce(Mathf.Lerp(force, 0, koof) * direction, mode);
        return Mathf.Lerp(damage, 0, koof);
    }
}