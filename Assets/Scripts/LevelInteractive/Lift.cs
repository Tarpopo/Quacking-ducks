using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class Lift : MonoBehaviour, ITick
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToActive;

    [SerializeField] private SimpleSound _signal;
    [SerializeField] private SimpleSound _music;
    [SerializeField] private AudioMixer _audioMixer;

    //[SerializeField]private int _nonColisLayer;
    //[SerializeField]private int _playerLayer;
    private Paralax _paralax;
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private Actor _playerActor;
    private BoxCollider2D _playerCollider;
    private Transform _playerTransform;
    private Transform _transform;
    private Animator _animator;
    private Vector3 _firstStartPosition;
    private Vector3 _firstEndPosition;

    private void Start()
    {
        _paralax = FindObjectOfType<Paralax>();
        _audioSource = GetComponent<AudioSource>();
        _transform = transform;
        _startPosition = _transform.position;
        _firstStartPosition = _startPosition;
        _firstEndPosition = _endPosition;
        _playerActor = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        _animator = GetComponent<Animator>();
        _playerTransform = _playerActor.transform;
        ManagerUpdate.AddTo(this);
        //print("lift start"+_transform.position);
    }

    public void Tick()
    {
        if (Vector3.Distance(_playerTransform.position, _transform.position) > _distanceToActive) return;
        _playerActor.DeactiveActor();
        _playerTransform.SetParent(_transform);
        //_playerTransform.gameObject.SetActive(false);
        _animator.Play("CloseDoor");
        _audioSource.PlayOneShot(_signal);
        _audioSource.PlayOneShot(_music);
        ManagerUpdate.RemoveFrom(this);
    }

    private void SetMainSound(bool isActive = false)
    {
        _audioMixer.SetFloat("MainVolume", isActive == false ? -80 : 0);
    }

    public void ActiveLift()
    {
        //_playerActor.gameObject.layer = _nonColisLayer;
        //_playerCollider.enabled = false;
        SetMainSound();
        StartCoroutine(Move(_endPosition));
    }

    public void ActiveActor()
    {
        //_playerCollider.enabled = true;
        //_playerActor.gameObject.layer = _playerLayer;
        //_playerTransform.gameObject.SetActive(true);
        _playerTransform.SetParent(null);
        _playerActor.ActiveActor();
        _endPosition = _startPosition;
        _startPosition = _transform.position;
        //print("lift endstart"+_startPosition);
    }

    public void SetStartState()
    {
        _startPosition = _firstStartPosition;
        _endPosition = _firstEndPosition;
        _transform.position = _firstStartPosition;
    }

    private IEnumerator Move(Vector3 position)
    {
        while (Vector3.Distance(_transform.position, _endPosition) > 0)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, position, _speed * Time.deltaTime);
            _paralax.SetMoving(true);
            yield return null;
        }

        _audioSource.Stop();
        _audioSource.PlayOneShot(_signal);
        _animator.Play("OpenDoor");
        SetMainSound(true);
        _paralax.SetMoving(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ManagerUpdate.AddTo(this);
        }
    }
}