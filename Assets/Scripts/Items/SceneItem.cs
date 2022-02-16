using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public abstract class SceneItem : MonoBehaviour,IStart,IPoolable,IDamagable
{
    public ItemData data;

    protected int Health;
    protected SpriteRenderer _spriteRenderer;
    protected Loader _loader;
    protected BoxCollider2D _collider;
    protected Rigidbody2D _rigidBody;
    protected AudioSource _audioSource;
    protected Transform _transform;
    protected int _baseSortingLayer;
    [SerializeField] protected int _whenTake;
    protected LayerMask _baseLayer;

    public void SetColliderState(bool state)
    {
        _collider.enabled = state;
    }

    public void SetBaseSortingLayer()
    {
        _spriteRenderer.sortingOrder = _baseSortingLayer;
    }

    public void ChangeSortingSprite(int value)
    {
        _spriteRenderer.sortingOrder = value;
    }
    
    public virtual void ApplyExplosionDamage(int damage,Vector2 pos,float force,float damageRadius)
    {
        _rigidBody.AddExplosionForce(damage,force,damageRadius,pos);
    }

    public virtual void PlayDamageSound(ISoundVisitor visitor){ }
    public virtual void OnSpawn()
    {
        _rigidBody.gravityScale = 1;
        Health = data.health;
        _spriteRenderer.sprite = data.fullSprite;
        _collider.enabled = true;
    }
    public virtual void OnDespawn()
    {
        _rigidBody.WakeUp();
        _collider.enabled = true;
    }

    public virtual void OnStart()
    {
        _loader = Toolbox.Get<Loader>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = transform;
        _baseSortingLayer = _spriteRenderer.sortingOrder;
        _baseLayer = gameObject.layer;
    }

    public virtual void TakeItem(Transform parent, Vector3 pos)
    {
        //_transform.rotation=Quaternion.identity;
        _rigidBody.gravityScale=0;
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        //_collider.enabled = false;
        gameObject.layer = _whenTake;
        _transform.SetParent(parent);
        _transform.position = pos;
    }
    public virtual void QuitItem(Vector3 dir)
    {
        _transform.position += dir;
        _transform.SetParent(null);
        _rigidBody.gravityScale=1;
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.AddForce(dir*4f,ForceMode2D.Impulse);
        gameObject.layer = _baseLayer;
        //_collider.enabled = true;
    }

    public virtual void Destroing()
    {
        _audioSource.PlaySound(data.destroy);
        //_rigidBody.gravityScale = 0;
        Invoke(nameof(DespawnGameObject),0.5f);
    }

    private void DespawnGameObject()
    {
        _loader.DespawnObject(gameObject);
    }

    public abstract void ApplyDamage(int damage, Vector2 pos, float force);
}
