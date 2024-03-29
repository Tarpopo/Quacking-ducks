﻿using DefaultNamespace;
using FSM;
using Managers;
using UnityEngine;

namespace Actors.Player
{
    public class Player : Actor, ITick, ITickFixed
    {
        [SerializeField] private float speed;

        public float jumpForce;

        //public float wallCheckDist;
        public float jumpRadius;

        public float takeRadius;

        //public Vector2 cicleDamagePos;
        public LayerMask ground;

        public LayerMask distructableLayer;

        // [SerializeField] private MoveButtons _moveButtons;
        // [SerializeField] private CustomButton _takeButton;
        // [SerializeField] private CustomButton _shootButton;
        // [SerializeField] private ButtonWithTick _jumpButton;
        [SerializeField] private int _killToComplete;
        [SerializeField] private Transform _wallCheckTransform;

        [SerializeField] private LayerMask _checkWallLayer;

        //[SerializeField] private Vector2 _WallCheckSize;
        [SerializeField] private float _wallCheckRadius;
        [SerializeField] private PlayerData _playerData;

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

        private ParticleManager _particleManager;
        private StateMachine<PlayerData> _stateMachine;

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
            data = Toolbox.Get<DataContainer>().CurrentDuck;
            _particleManager = Toolbox.Get<ParticleManager>();
            _cameraBehaviour = GetComponent<CameraBehaviour>();
            _startPosition = _transform.position;
            _weapon = GetComponent<Weapon>();
            _weapon.SetAudioSource(AudioSource);
            anima.Play(data.idle.name);
            //_particles = _loader.SpawnParticles(_transform.position);
            // ItemsSpawner.damagableObjects.Add(gameObject, this);
            _healthBar = GetComponent<HealthBar>();
            _healthBar.SetMaxHealth(_health, 2, null);
            _healthBar.SetHealthValue(_health);
            // _moveButtons.MoveAction.AddListener(Move);
            // _moveButtons.ButtonUp.AddListener(EndMove);
            // _shootButton.ButtonDown += AttackEnemy;
            // _jumpButton.ButtonUpdate = JumpUpdate;
            // _jumpButton.ButtonDown = JumpDown;
            // _takeButton.ButtonDown += TakeItem;
            //_takeButton.ButtonDown += SetTime;
            _weapon.SetBaseWeapon(true);
            var input = Toolbox.Get<InputManager>();
            input.OnShootDown += AttackEnemy;
            input.OnFButtonDown += TakeItem;
            input.OnMoveDown += SetMoveState;
            input.OnMoveUp += SetIdleState;
            input.OnJumpDown += SetJumpState;
            _stateMachine = new StateMachine<PlayerData>();
            _stateMachine.AddState(new PlayerIdle(_playerData, _stateMachine));
            _stateMachine.AddState(new PlayerMove(_playerData, _stateMachine));
            _stateMachine.AddState(new PlayerJump(_playerData, _stateMachine));
            _stateMachine.Initialize<PlayerIdle>();
            //_startPosition=_transform.position;
            // Toolbox.Get<EndLevelChecker>().SetCompleteCount(_killToComplete);
            // _jumpButton.ButtonDown = Jump;
            // _audio = Toolbox.Get<AudioManager>();
            //_weapon = BaseWeapon;
        }

        private void SetMoveState() => _stateMachine.ChangeState<PlayerMove>();

        private void SetIdleState() => _stateMachine.ChangeState<PlayerIdle>();

        private void SetJumpState()
        {
            if (CheckGround(jumpRadius) == false) return;
            _stateMachine.ChangeState<PlayerJump>();
        }
        // void OnSpawn()
        // {
        //     _loader.Spawn();
        // }
        // public void SetTime()
        // {
        //     print("setTime");
        //     Toolbox.Get<TimeScaler>().SetTimeScale();
        //     //anima.SetFloat("Speed",2);
        //     anima.speed = 3;
        // }

        public void Tick()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }

        public void TickFixed()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
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

            _weapon.Shoot(ItemsSpawner);
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
                    if (ItemsSpawner.Items.TryGetValue(itemCollider2D.gameObject, out _item))
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
            _particleManager.PlayParticle(data.deathParticles, _transform.position);
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
    }
}