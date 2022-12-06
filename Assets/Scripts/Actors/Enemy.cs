using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class Enemy : Actor,ITick
{
    public LayerMask whoIsEnemy;
    public Transform shootPos;
    public float enemySpeed;
    
    private float _colDownTime;
    private float _currentTime;
    private float _currentShootTime;
    private float _shootTime;
    private bool _seePlayer;
    private EnemyState _myState;
    //private Rigidbody2D _rigidBody;
    private Coroutine moveFunc;
    //private Loader _loader;
    private Weapon _weapon;
    private Transform _playerTransform;
    
    //private Transform _playerTransform;
    //private Animator _particles;
    protected override void StartGame()
    {
        _shootTime = 1.8f;
        //_rigidBody = GetComponent<Rigidbody2D>();
        _colDownTime = 2.2f;
        _currentTime = _colDownTime;
        //_loader = Toolbox.Get<Loader>();
        _weapon = GetComponent<Weapon>();
        _weapon.SetBaseWeapon();
        _weapon.SetAudioSource(AudioSource);
        // ItemsSpawner.damagableObjects.Add(gameObject,this);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //_loader.damagableObjects.Add(gameObject,this);
        //_particles = _loader.SpawnParticles(transform.position);
        SetState(EnemyState.Wait);
        
    }

    public void Tick()
    {
        if (_isDead) return;
        if (Physics2D.Raycast(shootPos.position, Vector2.right*transform.localScale.x, 2, whoIsEnemy))
        {
            if (!_seePlayer)
            {
                _seePlayer = true;
                anima.Play("EnemyDuckQuack");
                AudioSource.PlaySound(data.quackSound);
                _currentShootTime = 0.5f;
                if (moveFunc != null)
                {
                    StopCoroutine(moveFunc);
                }
            }
        }

        if (_seePlayer)
        {
            _transform.localScale = GetScale();
            if (_currentShootTime > 0) _currentShootTime -= Time.deltaTime;
            else
            {
                if (moveFunc != null)
                {
                    StopCoroutine(moveFunc);
                }
                
                SetState(EnemyState.Shoot);
                _currentShootTime = _shootTime;
            }
        }

        if(_currentTime>0)_currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _colDownTime;
            SetState();
        }
    }
    public override void ApplyDamage(int damage, Vector2 pos,float force)
    {
        if (moveFunc != null) StopCoroutine(moveFunc);
        base.ApplyDamage(damage, pos,force);
        //_rigidBody2D.AddForce(pos*0.02f,ForceMode2D.Impulse);
    }

    public override void ApplyExplosionDamage(int damage,Vector2 pos,float force,float damageRadius)
    {
        base.ApplyExplosionDamage(damage,pos,force,damageRadius);
        if (moveFunc != null) StopCoroutine(moveFunc);
    }

    private void SetState(EnemyState state=EnemyState.Empty)
    {
        if (state == EnemyState.Empty)
        {
            state = (EnemyState) Random.Range(0, 3);
        }

        switch (state)
        {
            case EnemyState.Wait:
                anima.Play("EnemyDuckIdle");
                _myState = EnemyState.Wait;
                //print("Idle");
                break;
            case EnemyState.Run:
                anima.Play("EnemyDuckRun");
                transform.localScale=GetScale();
                moveFunc=StartCoroutine(MovePosition());
                _myState = EnemyState.Run;
                //print("run");
                break;
            case EnemyState.Shoot:
                //print("shoot");
                anima.Play("EnemyDuckShoot");
                //BaseWeapon.Shoot(shootPos,_loader);
                AttackEnemy();
                _myState = EnemyState.Shoot;
                break;
            case EnemyState.ChangeDir:
                transform.localScale = GetScale();
                anima.Play("EnemyDuckIdle");
                _myState = EnemyState.ChangeDir;
                //print("change");
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    IEnumerator MovePosition()
    {
        while(_currentTime>0)
        {
            _rigidBody.position = Vector2.MoveTowards(_rigidBody.position,
                _rigidBody.position + Vector2.right * transform.localScale.x, enemySpeed*Time.deltaTime); 
            yield return null;
        }
    }

    private Vector3 GetScale()
    {
        if (_seePlayer)
        {
            return _transform.position.x - _playerTransform.position.x > 0?new Vector3(-1, 1, 1): Vector3.one;
        }
        var value=Random.Range(0, 2);
        return value == 0 ? new Vector3(-1,1,1) : Vector3.one;
    }

    protected override void Death()
    {
        // _particles.transform.position = transform.position;
        // _particles.Play("DuckFeathers");
        Toolbox.Get<EndLevelChecker>().AddDeathCount();
        ParticleManager.PlayParticle(data.deathParticles,transform.position);
        base.Death();
        if (moveFunc != null)
        {
            StopCoroutine(moveFunc);
            moveFunc = null;
        }
        Invoke(nameof(DeactiveEnemy),5.5f);
    }

    private void DeactiveEnemy()
    {
        gameObject.SetActive(false);
    }

    public override void AttackEnemy()
    { 
        anima.Play(data.ActorLight.name);
        _weapon.Shoot(ItemsSpawner);
    }
}
public enum EnemyState
{
    Wait,
    Run,
    ChangeDir,
    Shoot,
    Empty,
}
