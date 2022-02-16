using System;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Actor : MonoBehaviour, IDamagable, IBodySound
{
    public DataActor data;
    public bool _takeDamage = false;
    public float _spikeForce;
    
    protected float attackPosX;
    protected bool _isDead;
    protected Vector3 attackPos;
    
    protected Transform _transform;

    //protected Weapon BaseWeapon;
    protected SpriteRenderer Sprite;
    protected Collider2D enemy;
    protected Animator anima;
    protected AudioSource AudioSource;
    protected Rigidbody2D _rigidBody;
    protected int _health;
    protected Loader _loader;
    private BoxCollider2D _col;
    private Shader _shaderText;
    private Shader _shaderSpritesDefault;
    protected CoinDropper _coinDropper;
    
    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
        _col.size = data.colSize;
        _col.offset = data.colOffset;
        //print(colider.offset+" "+colider.size);
        //print(gameObject.name+gameObject.tag);
        ManagerUpdate.AddTo(this);
        AudioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        _shaderText = Shader.Find("GUI/Text Shader");
        _shaderSpritesDefault = Shader.Find("Sprites/Default");
        _health = data._health;
        _transform = transform;
        //_startPosition = _transform.position;
        // BaseWeapon = GetComponent<Weapon>();
        // BaseWeapon.SetAudioSource(AudioSource);
        // BaseWeapon.SetBulletCount();
        _loader = Toolbox.Get<Loader>();
        TryGetComponent(out _coinDropper);
        StartGame();
    }

    protected virtual void StartGame()
    {
        
    }

    protected virtual void Death()
    {
        if (_isDead) return;
        anima.Play(data.death.name);
        _isDead = true;
        //TurfCoins();
        if (_coinDropper != null) _coinDropper.DropCoins();
        ManagerUpdate.RemoveFrom(this);
        //_health = data._health;
    }

    // protected void TurfCoins()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         _loader.SpawnObject(_transform.position, ObjectId.Coin, true);
    //     }
    // }

    // public void Restart()
    // {
    //     if (!gameObject.activeSelf)
    //     {
    //         ManagerUpdate.AddTo(this);
    //         gameObject.SetActive(true);
    //     }
    // }

    public virtual void DeactiveActor()
    {
        _isDead = true;
        _rigidBody.velocity=Vector2.zero;
        _rigidBody.gravityScale = 0;
        _col.enabled = false;
        anima.Play(data.idle.name);
        //_rigidBody.gravityScale = 0;
    }

    public virtual void ActiveActor()
    {
        _isDead = false;
        _rigidBody.gravityScale = 1.5f;
        _col.enabled = true;
        anima.Play(data.idle.name);
        //_rigidBody.gravityScale = 1.5f;
    }

    public virtual void MakeActorAlive()
    {
        ActiveActor();
        _health = data._health;
        ManagerUpdate.AddTo(this);
    }

    public abstract void AttackEnemy();

    public void PlayDamageSound(ISoundVisitor visitor)
    {
        visitor.Visit(this);
    }

    private void ReduceHealth(int damage)
    {
        if (_isDead) return;
        _health -= damage;
        // data.curr_health = _health;
        if (_health > 0) return;
        Death();
    }
    private void SetWhiteSprite() 
    {
        Sprite.material.shader = _shaderText;
        Sprite.color = Color.white;
        Invoke(nameof(SetNormalSprite),0.1f);
    }
    
    private void SetNormalSprite()
    {
        Sprite.material.shader = _shaderSpritesDefault;
        Sprite.color = Color.white;
    }
    
    public virtual void ApplyDamage(int damage, Vector2 pos,float force)
    {
        var direction = _transform.position.x - pos.x > 0 ? Vector2.right : Vector2.left;
        _rigidBody.AddForce(direction * force,ForceMode2D.Impulse);
        SetWhiteSprite();
        ReduceHealth(damage);
    }

    public virtual void ApplyExplosionDamage(int damage,Vector2 pos,float force,float damageRadius)
    {
        SetWhiteSprite();
        ReduceHealth((int)_rigidBody.AddExplosionForce(damage,force,damageRadius,pos));
    }

    public void WalkSound()
    {
        var randomCount = Random.Range(0, data.stepSounds.Length);
        AudioSource.PlayOneShot(data.stepSounds[randomCount]);
        print("Its walk");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            ApplyDamage(2,other.transform.position,_spikeForce);
        }
    }

    // protected void DeactiveGameObject()
    // {
    //     this.gameObject.SetActive(false);
    // }

    // protected void PlaySound(SimpleSound simpleSound)
    // {
    //     AudioSource.volume = simpleSound.volumeSound;
    //     AudioSource.PlayOneShot(simpleSound.audioClip);
    // }
}