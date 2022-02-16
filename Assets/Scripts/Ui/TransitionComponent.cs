using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionComponent : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private UnityEvent _event;
    [SerializeField] private AnimationClip _transition;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayTransition()
    {
        _animator.Play(_transition.name);
    }

    public void DoAction()
    {
        _event?.Invoke();
        _event = null;
    }
}
