using System;
using UnityEngine;

[Serializable]
public class DamageableTriggerChecker : BaseTriggerChecker
{
    [SerializeField] private int _damageLayerIndex;

    // private Health _health;
    // [SerializeField] private float _visibleAngle;
    // public void TryApplyDamage(int damage)
    // {
    //     if (Vector3.Angle(transform.forward, _damageable.Target.position - transform.position) > _visibleAngle) return;
    //     _damageable.TakeDamage(transform.position, damage);
    // }

    protected override bool IsThisObject(GameObject gameObject) => false;
    // gameObject.layer.Equals(_damageLayerIndex) && gameObject.TryGetComponent(out _health);
}