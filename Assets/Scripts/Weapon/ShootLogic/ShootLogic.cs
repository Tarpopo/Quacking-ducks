using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;
public abstract class ShootLogic : ScriptableObject
{
    protected Animator _weaponAnimator;
    protected WeaponItem _weaponItem;
    protected AudioSource _audioSource;
    protected Transform _shootTransform;
    private Rigidbody2D _rigidbody;
    //protected ParticleSystem _particleSystem;
    public void SetParameters(Animator animator,WeaponItem weaponItem,AudioSource audioSource, Rigidbody2D rigidbody)
    {
        _weaponAnimator = animator;
        _weaponItem = weaponItem;
        _audioSource = audioSource;
        _rigidbody = rigidbody;
    }

    public void SetShootTransform(Transform transform)
    {
        _shootTransform = transform;
    }

    public virtual void Shoot(ISoundVisitor visitor,Loader loader) { }
    
    protected void TakeRecoil()
    {
        _rigidbody.AddForce(Vector2.left * (_weaponItem.transform.parent.localScale.x * _weaponItem.WeaponData.recoil),ForceMode2D.Impulse);
    }
}
