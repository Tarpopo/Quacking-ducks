using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : BulletComponent
{
    [SerializeField]private float _damageRadius;
    //[SerializeField] private float _force;
    private Transform _particleTransform;

    public override void OnSpawn()
    {
        _particleSystem=_loader.SpawnObject(_transform.position, ObjectId.RocketParticle, true).GetComponent<ParticleSystem>();
        _particleTransform = _particleSystem.transform;
        base.OnSpawn();
    }

    protected override void CheckCollision()
    {
        var hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, 0.05f,_hitable);
        if (hit==false) return;
        ManagerUpdate.RemoveFrom(this);
        //ParticleManager.PlayParticle(endShoot,_transform.position);
        _currentTime = 0;
        _loader.DespawnObject(gameObject);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Explotion();
    }
    
    public override void Tick()
    {
        base.Tick();
        _particleTransform.position = _transform.position;
        // if (Physics2D.Raycast(_transform.position, Vector3.right * _transform.localScale.x, 0.05f,hitable))
        // {
        //    Explotion(); 
        //    _loader.DespawnObject(gameObject);
        // }
        //_transform.Translate(Vector3.right * (_transform.localScale.x * (speed * Time.deltaTime)));
    }
    
    private void Explotion()
    {
        var colMass = Physics2D.OverlapCircleAll(_transform.position,_damageRadius,_hitable);
        for (int i = 0; i < colMass.Length; i++)
        {
            if (_loader.damagableObjects.TryGetValue(colMass[i].gameObject, out _item))
            { 
                _item.ApplyExplosionDamage(_damage,_transform.position,_force,_damageRadius);
            }
        }
    }

}
