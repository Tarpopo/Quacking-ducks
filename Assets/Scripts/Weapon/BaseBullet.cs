using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IPoolable, IStart, ITick
{
    public float speed;
    public float time;
    public ISoundVisitor Visitor;
    public LayerMask hitable;
    [SerializeField] protected int _damage;

    public AnimationClip endShoot;

    private float _currentTime;
    protected ItemsSpawner ItemsSpawner;
    protected IDamagable _item;
    protected Transform _transform;
    protected ParticleSystem _particleSystem;
    private ParticleManager _particleManager;

    public virtual void SetScale(int scale)
    {
        _transform.localScale = new Vector3(scale, 1, 1);
    }

    public virtual void OnStart()
    {
        //ManagerUpdate.AddTo(this);
        ItemsSpawner = Toolbox.Get<ItemsSpawner>();
        _transform = GetComponent<Transform>();
        _particleManager = Toolbox.Get<ParticleManager>();
    }

    public virtual void Tick()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            ItemsSpawner.DespawnObject(gameObject);
        }
    }

    public virtual void OnSpawn()
    {
        ManagerUpdate.AddTo(this);
        _currentTime = time;
        if (_particleSystem) _particleSystem.Play();
        print("Hey its bullet");
    }

    public virtual void OnDespawn()
    {
        ManagerUpdate.RemoveFrom(this);
        if (endShoot)
            _particleManager.PlayParticle(endShoot, _transform.position, 0.5f, scale: (int)_transform.localScale.x);
        _currentTime = time;
        Invoke(nameof(Despawn), 0.9f);
    }

    private void Despawn()
    {
        if (_particleSystem) ItemsSpawner.DespawnObject(_particleSystem.gameObject);
    }
}