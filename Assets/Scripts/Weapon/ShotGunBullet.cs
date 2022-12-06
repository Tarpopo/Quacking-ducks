using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShotGunBullet : BaseBullet
{
    [SerializeField] private float _shootLenght;
    private GameObject particleOcject;
    public override void OnSpawn()
    {
        particleOcject=ItemsSpawner.SpawnObject(_transform.position, ObjectId.ShutGunParticle, true);
        _particleSystem = particleOcject.GetComponent<ParticleSystem>();
        base.OnSpawn();
    }

    public override void SetScale(int scale)
    {
        base.SetScale(scale);
        particleOcject.transform.localScale=new Vector3(scale,1,1);
    }

    public override void Tick()
    {
        base.Tick();
        var hit = Physics2D.Raycast(_transform.position, Vector3.right * _transform.localScale.x, _shootLenght, hitable);
        if (hit)
        {
            // if (ItemsSpawner.damagableObjects.TryGetValue(hit.collider.gameObject, out _item))
            // {
            //     _item.ApplyDamage(1,transform.position,0.2f);
            //     _item.PlayDamageSound(Visitor);
            // }
            ItemsSpawner.DespawnObject(gameObject);
        }
        // _transform.Translate(Vector3.right * (_transform.localScale.x * (speed * Time.deltaTime)));
    }
}
