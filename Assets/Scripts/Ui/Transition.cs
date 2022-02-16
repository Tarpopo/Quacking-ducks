using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AnimationClip GateUp;
    [SerializeField] private AnimationClip GateDown;
    [SerializeField] private SimpleSound _door;
    private AudioSource _audioSource;
    private void Start()
    {
        OnStart();
    }

    public void PlayDoorSound()
    {
        _audioSource.PlaySound(_door);
    }

    public void OnStart()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void LoadMainSceneWithTime()
    {
        _animator.Play(GateDown.name);
        Invoke(nameof(LoadMainScene),0.9f);
    }

    public void LoadMainScene()
    {
        Toolbox.Get<SceneController>().LoadMenuScene();
    }

    public void PlayGateUp()
    {
        _animator.Play(GateUp.name);
    }

    public void PlayGateDown()
    {
        _animator.Play(GateDown.name);
    }
}
