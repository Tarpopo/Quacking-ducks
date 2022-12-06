using System.Collections;
using System.Collections.Generic;
using Interfaces.SoundsTypes;
using UnityEngine;
[CreateAssetMenu(menuName = "ShootLogic/ShutGunShoot")]
public class ShutGunShoot : ShootLogic
{
    public ObjectId bulletId;
    public int bulletsInOneShoot;
    public override void Shoot(ISoundVisitor visitor,ItemsSpawner itemsSpawner)
    {
        if (_weaponItem.CurrentBullet > 0)
        { 
            TakeRecoil();
            _weaponItem.CurrentBullet--;
            _audioSource.PlaySound(_weaponItem.WeaponData.shootSound);
            ParticleManager.PlayParticle(_weaponItem.WeaponData.Sleeve,_shootTransform.position,scale:(int)_shootTransform.parent.localScale.x);
            ParticleManager.PlayParticle(_weaponItem.WeaponData.ShootParticle,_shootTransform.position,scale:(int)_shootTransform.parent.localScale.x);
            _weaponAnimator.Play(_weaponItem.WeaponData.shootAnim.name);
            var shootPosition = _shootTransform.position;
            for (int i = 0; i < bulletsInOneShoot; i++)
            {
                var bulletComponent = itemsSpawner.SpawnObject(shootPosition,bulletId,false).GetComponent<BulletComponent>();
                bulletComponent.SetScale(_weaponItem.transform.parent.localScale);
                bulletComponent.StartTimer();
                bulletComponent.SetHitLayer(_weaponItem.WeaponData.HitLayer,_weaponItem.WeaponData.Damage,_weaponItem.WeaponData.Force);
                bulletComponent.SetVisitor(visitor);
                bulletComponent.gameObject.SetActive(true);
                shootPosition=new Vector3(shootPosition.x,shootPosition.y-0.03f,shootPosition.z);
            }
        }
        else _audioSource.PlaySound(_weaponItem.WeaponData.noAmmo);
    }
}
