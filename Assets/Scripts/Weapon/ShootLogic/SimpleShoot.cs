using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;
[CreateAssetMenu(menuName = "ShootLogic/SimpleShoot")]
public class SimpleShoot : ShootLogic
{
    public ObjectId bulletId;

    public override void Shoot(ISoundVisitor visitor,Loader loader)
    {
        if (_weaponItem.CurrentBullet > 0)
        { 
            TakeRecoil();
            _audioSource.PlaySound(_weaponItem.WeaponData.shootSound);
            ParticleManager.PlayParticle(_weaponItem.WeaponData.Sleeve,_shootTransform.position,scale:(int)_shootTransform.parent.localScale.x);
            ParticleManager.PlayParticle(_weaponItem.WeaponData.ShootParticle,_shootTransform.position,scale:(int)_shootTransform.parent.localScale.x);
            
            _weaponItem.CurrentBullet--;
            _weaponAnimator.Play(_weaponItem.WeaponData.shootAnim.name);

            var bulletComponent = loader.SpawnObject(_shootTransform.position,bulletId,false).GetComponent<BulletComponent>();
            bulletComponent.SetScale(_weaponItem.transform.parent.localScale);
            bulletComponent.StartTimer();
            bulletComponent.SetHitLayer(_weaponItem.WeaponData.HitLayer, _weaponItem.WeaponData.Damage,_weaponItem.WeaponData.Force);
            bulletComponent.SetVisitor(visitor);
            bulletComponent.gameObject.SetActive(true);
        }
        else _audioSource.PlaySound(_weaponItem.WeaponData.noAmmo);
    }
}

