using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DefaultNamespace;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLog : Actor, ITick
{
    [SerializeField] private float speed;

    public float jumpForce;

    //public float wallCheckDist;
    public float jumpRadius;

    public float takeRadius;

    //public Vector2 cicleDamagePos;
    public LayerMask ground;
    public LayerMask distructableLayer;
    [SerializeField] private MoveButtons _moveButtons;
    [SerializeField] private CustomButton _takeButton;
    [SerializeField] private CustomButton _shootButton;
    [SerializeField] private ButtonWithTick _jumpButton;
    [SerializeField] private int _killToComplete;
    [SerializeField] private Transform _wallCheckTransform;

    [SerializeField] private LayerMask _checkWallLayer;

    //[SerializeField] private Vector2 _WallCheckSize;
    [SerializeField] private float _wallCheckRadius;

    //public LayerMask Chest;
    //public LayerMask interactiveObj;
    //public WeaponData arm;
    //public GameObject Tree;
    public Transform shootPosition;

    //public Transform particleTransform;
    public Vector3 itemTransform;

    //public SpriteRenderer itemSprite;
    public ParticleSystem ParticleSystem;
    private CameraBehaviour _cameraBehaviour;

    private Vector3 _startPosition;
    private Vector3 _lastDeathPosition;

    private bool _isTakeItem;

    private float _currentReloadTime;

    //private Rigidbody2D _rigid;
    private Collider2D _jumpHit;

    private SceneItem _item;

    //private Loader _loader;
    private SpriteRenderer _weaponSprite;

    //private CameraBehaviour _cameraBehaviour;
    private AnimationClip _shootAnim;

    private AnimationClip _reloadAnim;

    //private Animator _weaponAnim;
    //private Animator _particles;
    //private ShootLogic shoot;
    //private ManagerPool _managerPool;
    private Weapon _weapon;

    private HealthBar _healthBar;
    private Vector2 _lastJumpPos;
    private Vector2 _posToJump;

    private bool _isGround;

    //private Vector3 _startPosition;
    // private AudioManager _audio;
    protected override void StartGame()
    {
        //_managerPool = Toolbox.Get<ManagerPool>();
        //_weaponSprite=GameObject.FindWithTag("Weapon").GetComponent<SpriteRenderer>();
        //_weaponAnim = _weaponSprite.GetComponent<Animator>();
        //SetArmWeapon();
        //_rigid = GetComponent<Rigidbody2D>();
        //_loader = Toolbox.Get<Loader>();
        //_cameraBehaviour = GetComponent<CameraBehaviour>();
        _cameraBehaviour = GetComponent<CameraBehaviour>();
        _startPosition = _transform.position;
        _weapon = GetComponent<Weapon>();
        _weapon.SetAudioSource(AudioSource);
        anima.Play(data.idle.name);
        //_particles = _loader.SpawnParticles(_transform.position);
        _loader.damagableObjects.Add(gameObject, this);
        _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxHealth(_health, 2, null);
        _healthBar.SetHealthValue(_health);
        _moveButtons.MoveAction.AddListener(Move);
        _moveButtons.ButtonUp.AddListener(EndMove);
        _shootButton.ButtonDown += AttackEnemy;
        _jumpButton.ButtonUpdate = JumpUpdate;
        _jumpButton.ButtonDown = JumpDown;
        _takeButton.ButtonDown += TakeItem;
        //_takeButton.ButtonDown += SetTime;
        _weapon.SetBaseWeapon(true);
        //_startPosition=_transform.position;
        Toolbox.Get<EndLevelChecker>().SetCompleteCount(_killToComplete);
        // _jumpButton.ButtonDown = Jump;
        // _audio = Toolbox.Get<AudioManager>();
        //_weapon = BaseWeapon;
    }

    // void OnSpawn()
    // {
    //     _loader.Spawn();
    // }
    public void SetTime()
    {
        print("setTime");
        Toolbox.Get<TimeScaler>().SetTimeScale();
        //anima.SetFloat("Speed",2);
        anima.speed = 3;
    }

    public void Tick()
    {
        if (_isDead) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //AttackEnemy();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            var obj = _loader.SpawnObject(ObjectId.Tree, true);
            obj.transform.position = _transform.position;
        }

        if (Input.GetButtonDown("Horizontal")) Move((int)Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Space)) JumpDown();
        if (Input.GetKey(KeyCode.Space)) JumpUpdate();
        if (Input.GetKeyDown(KeyCode.K)) AttackEnemy();
        if (Input.GetKeyDown(KeyCode.J)) TakeItem();
    }

    public override void MakeActorAlive()
    {
        base.MakeActorAlive();
        _transform.position = CheckGround(0.07f) ? _lastDeathPosition : _startPosition;
        _rigidBody.constraints = RigidbodyConstraints2D.None;
        _rigidBody.freezeRotation = true;
        _healthBar.SetHealthValue(_health);
    }

    public override void ApplyDamage(int damage, Vector2 pos, float force)
    {
        base.ApplyDamage(damage, pos, force);
        _cameraBehaviour.Shake(0.1f, 1f);
        _healthBar.SetHealthValue(_health);
    }

    public override void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius)
    {
        base.ApplyExplosionDamage(damage, pos, force, damageRadius);
        _cameraBehaviour.Shake(1f, 1.5f);
        _healthBar.SetHealthValue(_health);
    }

    public override void AttackEnemy()
    {
        if (_isDead || _weapon.IsBaseWeapon() && _isTakeItem) return;
        if (_weapon.IsBulletShoot())
        {
            _cameraBehaviour.Shake(0.1f, 0.3f);
            anima.Play(data.ActorLight.name);
        }

        _weapon.Shoot(_loader);
    }

    private void QuitItem(Vector3 dir)
    {
        if (_weapon.TryQuitWeapon() == false)
        {
            _item.QuitItem(dir);
        }

        _isTakeItem = false;
    }

    private void TakeItem()
    {
        if (_isTakeItem)
        {
            QuitItem(new Vector3(itemTransform.x * _transform.localScale.x, itemTransform.y, 0));
            //_weapon.SetItemFlag(false);
        }
        else
        {
            var itemCollider2D = Physics2D.OverlapCircle(_transform.position, takeRadius, distructableLayer);
            if (itemCollider2D)
            {
                if (_loader.Items.TryGetValue(itemCollider2D.gameObject, out _item))
                {
                    if (itemCollider2D.CompareTag("Weapon")) _weapon.SetWeapon((WeaponItem)_item);
                    _item.TakeItem(_transform, _transform.position);
                    _isTakeItem = true;
                }
            }
            else
            {
                Quack();
            }
        }
    }

    // private void Move()
    // {
    //     if (Input.GetButtonDown("Horizontal"))
    //     {
    //         ParticleSystem.Play();
    //     }
    //
    //     if (Input.GetButton("Horizontal"))
    //     {
    //         var hor = Input.GetAxis("Horizontal");
    //         var transform1 = _transform;
    //         transform1.localScale = hor > 0 ? Vector3.one : new Vector3(-1, 1, 1);
    //         //if (Physics2D.Raycast(_transform.position, Vector2.right * transform1.localScale.x,0.07f ,ground)) return;
    //         _rigidBody.velocity=new Vector2(speed*transform1.localScale.x,_rigidBody.velocity.y);
    //         _jumpHit = Physics2D.OverlapCircle(_transform.position,jumpRadius ,ground);
    //         if(_jumpHit)anima.Play(data.run.name);
    //         else
    //         {
    //             anima.Play(data.idle.name);
    //         }
    //     }
    //     
    //     if (Input.GetButtonUp("Horizontal"))
    //     {
    //         _rigidBody.velocity=new Vector2(0,_rigidBody.velocity.y);
    //         anima.Play(data.idle.name);
    //     }
    // }
    private void EndMove()
    {
        if (_isDead) return;
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        anima.Play(data.idle.name);
    }

    public override void DeactiveActor()
    {
        if (_isTakeItem)
        {
            //QuitItem(new Vector3(itemTransform.x*_transform.localScale.x,itemTransform.y,0));
            _item.SetColliderState(false);
            _item.ChangeSortingSprite(1);
        }

        EndMove();
        base.DeactiveActor();
    }

    public override void ActiveActor()
    {
        base.ActiveActor();
        if (_isTakeItem == false) return;
        _item.SetColliderState(true);
        _item.SetBaseSortingLayer();
        //_rigidBody.gravityScale = 1.5f;
    }

    private void Move(int direction)
    {
        print(direction);
        if (_isDead) return;
        _transform.localScale = new Vector3(direction, 1, 1);
        if (Physics2D.OverlapCircle(_wallCheckTransform.position, _wallCheckRadius, _checkWallLayer))
        {
            EndMove();
            return;
        }

        _rigidBody.velocity = new Vector2(Time.unscaledDeltaTime * speed * direction, _rigidBody.velocity.y);
        anima.Play(CheckGround(jumpRadius) ? data.run.name : data.idle.name);
        print("move");
    }

    private bool CheckGround(float radius)
    {
        return Physics2D.OverlapCircle(_transform.position, radius, ground) != null;
    }

    private void JumpDown()
    {
        if (_isDead) return;
        ParticleSystem.Play();
        //_lastJumpPos = _rigidBody.position;
        //_posToJump=Vector2.up;
        _isGround = CheckGround(jumpRadius);
        if (_isGround)
        {
            _rigidBody.velocity = Vector2.up * jumpForce; //поправь это, нужен вектор в 45
        }
    }

    public void JumpUpdate()
    {
        if (_isDead) return;

        if (_isGround == false) return;
        _rigidBody.velocity = _rigidBody.velocity.normalized * jumpForce;

        //_posToJump = _rigidBody.position - _lastJumpPos;
    }

    private void Quack()
    {
        AudioSource.PlayOneShot(data.quackSound);
        anima.Play(data.quack.name);
    }

    protected override void Death()
    {
        // _particles.transform.position = _transform.position;
        // _particles.Play("DuckFeathers");
        _lastDeathPosition = _transform.position;
        ParticleManager.PlayParticle(data.deathParticles, _transform.position);
        if (_isTakeItem)
        {
            QuitItem(itemTransform * _transform.localScale.x);
        }

        //_rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        base.Death();
        Toolbox.Get<EndLevelChecker>().LoadDeathScreenAnimation();
        Invoke(nameof(FreezeRigidBody), 0.4f);
    }

    private void FreezeRigidBody()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, jumpRadius);
        //Gizmos.DrawWireCube(_wallCheckTransform.position,_WallCheckSize);
        Gizmos.DrawWireSphere(_wallCheckTransform.position, _wallCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPosition.position, jumpRadius);
    }

    // public override void ApplyDamage(int damage, Vector2 pos,float force)
    // {
    //     //_rigid.AddExplosionForce(5,0.6f,pos);
    //     base.ApplyDamage(1, pos,force);
    // }

    // private void SetWeapon()
    // {
    //     _weapon = _item.GetItemObject<Weapon>();
    //     _weapon.SetAudioSource(AudioSource);
    //     _shootAnim = _weapon.weaponData.shootAnim;
    //     _weaponAnim.Play(_weapon.weaponData.idleAnim.name);
    //     _reloadAnim = _weapon.weaponData.reloadAnim;
    //     shootPosition.position =_transform.position+new Vector3(_weapon.weaponData.shootPos.x*_transform.localScale.x, 
    //         _weapon.weaponData.shootPos.y,0);
    //     
    // }
    //  private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag.Equals("Spikes")&&!_takeDamage)
    //     {
    //         ApplyDamage(1,Vector2.up,0.5f);
    //         _takeDamage = true;
    //     }
    // }
}