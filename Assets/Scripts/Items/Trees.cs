using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : SceneItem
{
    public float force;
    public Transform rootTree;
    
    private bool _isDead;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        base.OnStart();
        //Health = data.health;
        _spriteRenderer.sprite = data.fullSprite;
        rootTree.gameObject.GetComponent<SpriteRenderer>().sprite = data.halfSprite;
        _loader.damagableObjects.Add(gameObject,this);
        _loader.Items.Add(gameObject,this);
    }
    public override void OnSpawn() { }
    public override void OnDespawn() { }

    public override void TakeItem(Transform parent,Vector3 pos) { }
    public override void QuitItem( Vector3 dir) { }
    
    public override void Destroing() {}
    
    public override void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius)
    {
        _rigidBody.gravityScale = 1;
        gameObject.layer = _whenTake;
        base.ApplyExplosionDamage(damage, pos, force, damageRadius);
        Invoke(nameof(SetZeroGravity),1);
    }

    public override void ApplyDamage(int damage, Vector2 pos,float force)
    {
        var direction = transform.position.x - pos.x > 0 ? Vector2.right : Vector2.left;
        _rigidBody.gravityScale = 1;
        // var obj = _loader.SpawnObject(ObjectId.Root,true);
        // obj.transform.position = transform.position;
        rootTree.transform.parent = null;
        _rigidBody.AddForce(direction*force,ForceMode2D.Impulse);
        //_spriteRenderer.sprite = data.halfSprite;
        gameObject.layer = _whenTake;
        Invoke(nameof(SetZeroGravity),1);
    }

    private void SetZeroGravity()
    {
        _rigidBody.velocity=Vector2.zero;
        _rigidBody.gravityScale = 0;
    }
}
