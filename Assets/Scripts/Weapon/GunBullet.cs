using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : BaseBullet
{
    
    public override void Tick()
    {
        base.Tick();
        var hit = Physics2D.Raycast(_transform.position, Vector3.right * _transform.localScale.x, 0.05f,hitable);
        if (hit)
        {
            if (_loader.damagableObjects.TryGetValue(hit.collider.gameObject, out _item))
            {
                _item.ApplyDamage(_damage,transform.position,0.2f);
                _item.PlayDamageSound(Visitor);
            }

            //ParticleManager.PlayParticle(endShoot,_transform.position);
            _loader.DespawnObject(gameObject);
        }
        _transform.Translate(Vector3.right * (_transform.localScale.x * (speed * Time.deltaTime)));
    }
    
}
