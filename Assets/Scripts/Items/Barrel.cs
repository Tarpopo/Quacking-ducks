using System;
using System.Runtime.InteropServices.WindowsRuntime;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public class Barrel : SceneItem, IBarrelSound
{
    public int timeToExp;
    public Transform _smokeTransform;
    private IDamagable _distractable;
    private ParticleObject _particleObject;
    private ParticleManager _particleManager;
    public float damageRadius;

    [SerializeField] private float _force;
    [SerializeField] private int _damage;

    public LayerMask layer;
    public LayerMask distr;
    public AnimationClip smoke;

    private void Start()
    {
        _particleManager = Toolbox.Get<ParticleManager>();
        base.OnStart();
        Health = data.health;
        _spriteRenderer.sprite = data.fullSprite;
        _audioSource = GetComponent<AudioSource>();
        // ItemsSpawner.damagableObjects.Add(gameObject, this);
        ItemsSpawner.Items.Add(gameObject, this);
    }

    // public override void OnStart()
    // {
    //     base.OnStart();
    //     _audioSource = GetComponent<AudioSource>();
    // }

    public override void ApplyDamage(int damage, Vector2 pos, float force)
    {
        if (damage >= 100) Health = 0;
        Health--;
        if (Health <= 0)
        {
            Destroing();
            return;
        }

        if (Health == 1)
        {
            _audioSource.PlaySound(data.full);
            print("barrel hit");
            //_rigidBody.AddForce(((Vector2)_transform.position-pos)*force,ForceMode2D.Impulse);
            _particleObject =
                _particleManager.PlayDetachedParticle(smoke, _smokeTransform.position, timeToExp, transform, Destroing);
            //ParticleManager.PlayParticle(smoke,_smokeTransform.position,timeToExp,transform,Destroing);
            _spriteRenderer.sprite = data.halfSprite;
        }
    }

    public override void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius)
    {
        Health = 0;
        Destroing();
    }

    public override void Destroing()
    {
        //base.Destroing();
        _audioSource.PlaySound(data.destroy);
        _rigidBody.gravityScale = 0;
        if (_particleObject != null)
        {
            _particleObject.func = null;
            _particleObject.obj.SetActive(false);
        }

        _rigidBody.Sleep();
        _collider.enabled = false;
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layer);
        var position = hit ? (Vector3)hit.point : _transform.position;
        ParticleManager.PlayParticle(data.destroyAnim, position);
        _spriteRenderer.sprite = null;
        var colMass = Physics2D.OverlapCircleAll(_transform.position, damageRadius, distr);
        for (int i = 0; i < colMass.Length; i++)
        {
            if (colMass[i].TryGetComponent(out _distractable) == false) continue;
            _distractable.ApplyExplosionDamage(_damage, _transform.position, _force, damageRadius);

            // if (ItemsSpawner.damagableObjects.TryGetValue(colMass[i].gameObject, out _distractable))
            // { 
            //     _distractable.ApplyExplosionDamage(_damage,_transform.position,_force,damageRadius);
            // }
        }

        Invoke(nameof(DeactiveObject), 0.5f);
        print("BarrelIsDestroying");
        //dgameObject.SetActive(false);
    }

    private void DeactiveObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    public override void PlayDamageSound(ISoundVisitor visitor)
    {
        visitor.Visit(this);
    }
}