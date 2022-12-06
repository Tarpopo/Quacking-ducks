using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

[CreateAssetMenu(menuName = "ShootLogic/KnifeShoot")]
public class KnifeShoot : ShootLogic
{
    private IDamagable _distractable;
    [SerializeField] private int _damage;
    [SerializeField] private float _force;

    //public LayerMask layer;
    public float radius;

    public override void Shoot(ISoundVisitor visitor, ItemsSpawner itemsSpawner)
    {
        //TakeRecoil();
        _weaponAnimator.Play(_weaponItem.WeaponData.shootAnim.name);
        _audioSource.PlaySound(_weaponItem.WeaponData.shootSound);
        var hit2D = Physics2D.OverlapCircleAll(_shootTransform.position, radius, _weaponItem.WeaponData.HitLayer);
        foreach (var t in hit2D)
        {
            // if (!itemsSpawner.damagableObjects.TryGetValue(t.gameObject, out _distractable)) continue;
            if (t.TryGetComponent(out _distractable) == false) continue;
            _distractable.ApplyDamage(_damage, _shootTransform.parent.transform.position, _force);
            _distractable.PlayDamageSound(visitor);
        }
    }
}