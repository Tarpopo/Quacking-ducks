using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

namespace Actors.Boss
{
    public class Boss : Actor, ITick
    {
        [SerializeField] private float _force;
        [SerializeField] private int _damage;
        private Transform _playerTransform;
        public Transform HitTransform;
        public AnimationClip Fury;
        private IDamagable _damagable;
        public float DamageRadius;

        public LayerMask layer;

        //private Loader _loader;
        public List<Vector3> ShootPos;
        [SerializeField] private EnemyAttack[] Attacks;
        private HealthBar _healthBar;
        private bool _isNoDamage;

        protected override void StartGame()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            //_loader = Toolbox.Get<Loader>();
            var components = anima.GetBehaviours<BossRun>();
            _healthBar = GetComponent<HealthBar>();
            _healthBar.SetMaxHealth(_health, 2, Death);
            _healthBar.SetHealthValue(_health);
            foreach (var VARIABLE in components)
            {
                VARIABLE.RigidBody = _rigidBody;
                VARIABLE.Boss = this;
                VARIABLE.PlayerTransform = _playerTransform;
                VARIABLE.Transform = _transform;
            }

            // ItemsSpawner.damagableObjects.Add(gameObject, this);
        }

        private void SetFuryMod()
        {
            _isNoDamage = false;
            anima.Play(Fury.name);
        }

        protected override void Death()
        {
            if (_health > 0)
            {
                _isNoDamage = true;
                anima.Play(data.death.name);
                //_rigidBody.AddForce((_transform.position-_playerTransform.position).normalized*,ForceMode2D.Impulse);
                ParticleManager.PlayParticle(data.deathParticles, transform.position);
                _coinDropper.DropCoins();
                Invoke(nameof(SetFuryMod), 2f);
                return;
            }

            base.Death();
            Toolbox.Get<EndLevelChecker>().AddDeathCount();
        }

        public override void ApplyDamage(int damage, Vector2 pos, float force)
        {
            if (_isNoDamage) return;
            base.ApplyDamage(damage, pos, force);
            _healthBar.SetHealthValue(_health);
        }

        public override void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius)
        {
            if (_isNoDamage) return;
            base.ApplyExplosionDamage(damage, pos, force, damageRadius);
            _healthBar.SetHealthValue(_health);
        }

        public override void AttackEnemy()
        {
            throw new System.NotImplementedException();
        }

        public void Hit(BossAttackType state)
        {
            HitTransform.localPosition = Attacks[(int)state].ShootPosition;
            var hit2D = Physics2D.OverlapCircleAll(HitTransform.position, DamageRadius, layer);
            foreach (var t in hit2D)
            {
                // if (ItemsSpawner.damagableObjects.TryGetValue(t.gameObject, out _damagable) == false) continue;
                if (t.TryGetComponent(out _damagable) == false) continue;
                _damagable.ApplyDamage(Attacks[(int)state].Damage, HitTransform.position, _force);
                //_damagable.PlayDamageSound(visitor);
            }

            AudioSource.PlayOneShot(Attacks[(int)state].AttackSound);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                anima.Play(Fury.name);
                ApplyDamage(1, _transform.position, 0.05f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(HitTransform.position, DamageRadius);
        }
    }

    public enum BossStates
    {
        Attack,
        StrongAttack,
        Idle,
        Empty,
    }

    public enum BossAttackType
    {
        Uppercut,
        Kick,
        EasyHit,
        StrongHit,
    }

    [Serializable]
    public struct EnemyAttack
    {
        public Vector3 ShootPosition;
        public int Damage;
        public SimpleSound AttackSound;
    }
}