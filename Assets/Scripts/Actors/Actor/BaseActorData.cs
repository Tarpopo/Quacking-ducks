using System;
using UnityEngine;

[Serializable]
public class BaseActorData
{
    public DataActor DataActor => _dataActor;
    public Transform Transform => _transform;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Animator Animator => _animator;
    public AudioSource AudioSource => _audioSource;
    public Rigidbody2D Rigidbody2D => _rigidBody;
    public BoxCollider2D BoxCollider2D => _collider;
    public float MoveSpeed => _moveSpeed;

    [SerializeField] private DataActor _dataActor;
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _moveSpeed;
}