using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoseEnemy : Actor, ITick
{
    public LayerMask whoIsEnemy;
    public Transform shootPos;
    public float enemySpeed;
    public float ignoreDistance;
    public float attackDistance;
    public float AttackRadius;
    public bool isBasukaWeapon;
    
    [SerializeField] private float _colDownTime;
    //private float _currentTime;
    private float _currentShootTime;
    [SerializeField] private float _shootTime;
     private bool _seePlayer;
    //private EnemyState _myState;
    //private Rigidbody2D _rigidBody;
    private Coroutine moveFunc;
    //private Loader _loader;
    private Weapon _weapon;
    private Transform _playerTransform;

    private Timer _shootTimer;
    private Timer _stateTimer;
    private Timer _movingTimer;
    
    protected override void StartGame()
    {
        base.StartGame();
        // _shootTime = 1.8f;
        // _colDownTime = 2.2f;
        _shootTimer = gameObject.AddComponent<Timer>();
        _stateTimer = gameObject.AddComponent<Timer>();
        _shootTimer.StartTimer(null,_shootTime);
        _stateTimer.StartTimer(null,_colDownTime);
        
        //_currentTime = _colDownTime;
        //_loader = Toolbox.Get<Loader>();
        // ItemsSpawner.damagableObjects.Add(gameObject,this);
        _weapon = GetComponent<Weapon>();
        _weapon.SetAudioSource(AudioSource);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _transform = transform;
        _weapon.SetBaseWeapon();
        //if(isBasukaWeapon)_weapon.SetWeapon();
        SetState(EnemyState.Wait);
    }

    public void Tick()
    {
        if (_isDead) return;
        var distanceToPlayer = Vector2.Distance(_playerTransform.position, _transform.position);
        
        //if (_currentShootTime > 0) _currentShootTime -= Time.deltaTime;
        //if(_currentTime>0)_currentTime -= Time.deltaTime;
        
        if (distanceToPlayer<=ignoreDistance)
        {
            if(moveFunc!=null)StopCoroutine(moveFunc);
            SetScale();
            if (distanceToPlayer > attackDistance)
            {
                anima.Play("GoseFuryWalk");
                MoveToPlayer();
            }
            else
            {
                if (_shootTimer.GetIsTimerActive()) return;
                SetState(EnemyState.Shoot);
                print("GoseShoot");
                _shootTimer.StartTimer(null, _shootTime);
                //_currentShootTime = _shootTime;
            }
        }
        else
        {
            if (_stateTimer.GetIsTimerActive()) return;
            _stateTimer.StartTimer(null, _colDownTime);
            SetState();
        }
    }

    private void SetState(EnemyState state=EnemyState.Empty)
    {
        if(moveFunc!=null)StopCoroutine(moveFunc);
        if (state == EnemyState.Empty)
        {
            state = (EnemyState) Random.Range(0, 3);
        }
        switch (state)
        {
            case EnemyState.Wait:
                anima.Play("GoseIdle");
                //_myState = EnemyState.Wait;
                print("Idle");
                break;
            case EnemyState.Run:
                anima.Play("GoseWalk");
                transform.localScale=GetScale();
                moveFunc=StartCoroutine(MovePosition());
                //_myState = EnemyState.Run;
                //print("run");
                break;
            case EnemyState.Shoot:
                //print("shoot");
                //anima.Play("GoseAttack");
                //BaseWeapon.Shoot(shootPos,_loader);
                AttackEnemy();
                //_myState = EnemyState.Shoot;
                break;
            case EnemyState.ChangeDir:
                _transform.localScale = GetScale();
                anima.Play("GoseIdle2");
                //_myState = EnemyState.ChangeDir;
                //print("change");
                break;
            
            //default:
                //throw new ArgumentOutOfRangeException();
        }
    }

    public override void AttackEnemy()
    {
        if(_weapon.IsBulletShoot())anima.Play(data.ActorLight.name);
        _weapon.Shoot(ItemsSpawner);
    }
    IEnumerator MovePosition()
    {
        while(_stateTimer.GetIsTimerActive())
        {
            _rigidBody.position = Vector2.MoveTowards(_rigidBody.position,
                _rigidBody.position + Vector2.right * _transform.localScale.x, enemySpeed*Time.deltaTime); 
            yield return null;
        }
    }

    protected override void Death()
    {
        base.Death();
        Toolbox.Get<EndLevelChecker>().AddDeathCount();
        ParticleManager.PlayParticle(data.deathParticles,_transform.position);
        Invoke(nameof(DeactiveEnemy),5.5f);
    }

    private void MoveToPlayer()
    {
        _rigidBody.position = Vector2.MoveTowards(_rigidBody.position,
            _playerTransform.position, enemySpeed*Time.deltaTime);
    }

    private void SetScale()
    { 
        _transform.localScale = _transform.position.x - _playerTransform.position.x > 0
            ? new Vector3(-1, 1, 1)
            : Vector3.one;
    }

    private Vector3 GetScale()
    {
        var value=Random.Range(0, 2);
        return value == 0 ? new Vector3(-1,1,1) : Vector3.one;
    }
    private void DeactiveEnemy()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPos.position, AttackRadius);
        Gizmos.DrawWireSphere(transform.position,ignoreDistance);
        Gizmos.DrawWireSphere(transform.position,attackDistance);
    }
}

