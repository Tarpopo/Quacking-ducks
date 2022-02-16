using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public class Bullet : MonoBehaviour,IStart,IPoolable
{
    public float speed;
    public float time;
    public SimpleSound barrelSound;
    public SimpleSound chestSound;
    public SimpleSound bodySound;
    public LayerMask hitable;
    public ISoundVisitor Visitor;
    public bool IsRocket;
    public AnimationClip endShoot;
    
    private float _currentTime;
    private bool _isTakeItem;
    private ManagerPool _pool;
    private Loader _loader;
    private IDamagable _item;
    private AudioSource _audioSource;
    private Animator _animator;
    private Transform _rocketParticleTransform;
    private ParticleSystem _system;
    public void OnStart()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _pool = Toolbox.Get<ManagerPool>();
        _loader = Toolbox.Get<Loader>();
        _currentTime = time;
    }
    public void OnSpawn()
    {
        if(IsRocket)_rocketParticleTransform = _loader.SpawnObject(transform.position,ObjectId.ShutGunParticle,true).transform;
        _system=_rocketParticleTransform.GetComponent<ParticleSystem>();
        _rocketParticleTransform.transform.position = transform.position;
        _system.Play();

        //if(_system.isPlaying)_system.Stop();
        //_system.Play();

    }
    
    private void Update()
    {
        if (_currentTime <= 0)
        {
            Delete();
            return;
        }
        
        //if(IsRocket)_rocketParticleTransform.position=transform.position;
        
        var hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, 0.05f,hitable);
        if (hit&&!_isTakeItem)
        {
            _isTakeItem = true;
            if (_loader.damagableObjects.TryGetValue(hit.collider.gameObject, out _item))
            { 
                if(IsRocket)_item.ApplyExplosionDamage(4,transform.position,0.5f,0.55f);
                else _item.ApplyDamage(1,transform.position,0.2f);
                _item.PlayDamageSound(Visitor);
            }
            Delete();
            print("hit");
        }
        transform.Translate(Vector3.right*transform.localScale.x * (speed * Time.deltaTime));
        _currentTime -= Time.deltaTime;
    }

    private void PlaySound(AudioClip clip,float volume )
    {
        _audioSource.volume = volume;
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    private void Delete()
    {
        ParticleManager.PlayParticle(endShoot,transform.position,0.5f);
        _currentTime = time;
        //if(_animator!=null) _animator.Play("SimpleBulletEnd");
        _pool.Despawn(PoolType.Entities,gameObject);
    }

    public void Despawn()
    {
        _isTakeItem = false;
        _loader.DespawnObject(_rocketParticleTransform.gameObject);
    }

    public void OnDespawn()
    {
        if (IsRocket)
        {
            _rocketParticleTransform.parent = null;
            Invoke(nameof(Despawn),2f);
        }
    }
}
