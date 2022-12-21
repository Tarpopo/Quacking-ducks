using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AnimatorOverrideController _controller;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetController();
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetController()
    {
        _animator.runtimeAnimatorController = _controller;
    }
}