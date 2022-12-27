using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public class BulletComponent : MonoBehaviour, IPoolable, ITick
{
    protected Transform _transform;
    protected float _currentTime;
    public AnimationClip endShoot;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDestroy;
    protected LayerMask _hitable;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _force;
    protected ItemsSpawner ItemsSpawner;
    protected IDamagable _item;
    protected ParticleSystem _particleSystem;
    private ISoundVisitor Visitor;
    private ParticleManager _particleManager;

    private void Awake()
    {
        _transform = transform;
        ItemsSpawner = Toolbox.Get<ItemsSpawner>();
        _particleManager = Toolbox.Get<ParticleManager>();
    }

    public void SetHitLayer(LayerMask layer, int damage, float force)
    {
        _hitable = layer;
        _damage = damage;
        _force = force;
    }

    public void SetVisitor(ISoundVisitor soundVisitor)
    {
        Visitor = soundVisitor;
    }

    public void SetScale(Vector3 localScale)
    {
        _transform.localScale = localScale;
    }

    public void UpdateTime()
    {
        _currentTime -= Time.deltaTime;
    }

    public void UpdatePosition()
    {
        if (IsBulletActive() == false)
        {
            ItemsSpawner.DespawnObject(gameObject);
            //ManagerUpdate.RemoveFrom(this);
            return;
        }

        CheckCollision();
        _transform.Translate(Vector3.right * (_transform.localScale.x * (_speed * Time.deltaTime)));
    }

    protected virtual void CheckCollision()
    {
        var hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, 0.05f, _hitable);
        if (hit == false) return;
        if (hit.collider.TryGetComponent<IDamagable>(out var damageable))
        {
            damageable.ApplyDamage(_damage, transform.position, _force);
            damageable.PlayDamageSound(Visitor);
        }

        // if (ItemsSpawner.damagableObjects.TryGetValue(hit.collider.gameObject, out _item))
        // {
        //     _item.ApplyDamage(_damage, transform.position, _force);
        //     _item.PlayDamageSound(Visitor);
        // }

        //ParticleManager.PlayParticle(endShoot,_transform.position);
        _currentTime = 0;
        ItemsSpawner.DespawnObject(gameObject);
    }

    public virtual void Tick()
    {
        // if (IsBulletActive() == false)
        // {
        //     _loader.DespawnObject(gameObject);
        // }
        UpdatePosition();
        UpdateTime();
    }

    public void StartTimer()
    {
        _currentTime = _timeToDestroy;
    }

    public bool IsBulletActive()
    {
        return _currentTime > 0;
    }

    public virtual void OnSpawn()
    {
        ManagerUpdate.AddTo(this);
        if (_particleSystem) _particleSystem.Play();
    }

    public virtual void OnDespawn()
    {
        ManagerUpdate.RemoveFrom(this);
        if (endShoot)
            Toolbox.Get<ParticleManager>()
                .PlayParticle(endShoot, transform.position, 0.5f, scale: (int)transform.localScale.x);
        Invoke(nameof(Despawn), 0.9f);
    }

    private void Despawn()
    {
        if (_particleSystem) ItemsSpawner.DespawnObject(_particleSystem.gameObject);
    }
}