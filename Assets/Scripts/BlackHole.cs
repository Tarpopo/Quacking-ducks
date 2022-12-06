using DefaultNamespace;
using Triggers;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private InterfaceTriggerChecker<IDamagable> _triggerChecker;

    private void Start()
    {
        _triggerChecker = new InterfaceTriggerChecker<IDamagable>();
        _triggerChecker.OnGetObject += ApplyDamage;
    }

    private void OnDisable() => _triggerChecker.OnGetObject -= ApplyDamage;

    private void ApplyDamage() => _triggerChecker.Interface.ApplyDamage(100, Vector2.zero, 0);

    private void OnTriggerEnter2D(Collider2D other) => _triggerChecker.OnTriggerEnter2D(other);
}