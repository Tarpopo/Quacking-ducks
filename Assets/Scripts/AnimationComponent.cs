using System;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

[Serializable]
public class AnimationComponent
{
#if UNITY_EDITOR
    [SerializeField] private AnimatorController _animatorController;
#endif
    [SerializeField] private Animator _animator;
    private UnitAnimations _animations;
    private UnitAnimations _currentState = UnitAnimations.Idle;

#if UNITY_EDITOR
    private void UpdateAnimator()
    {
        _animatorController.GenerateAnimatorControllerFromExist(_animations, true);
    }
#endif

    public void PlayAnimation(UnitAnimations animationType)
    {
        _animator.SetBool(_currentState.ToString(), false);
        _currentState = animationType;
        _animator.SetBool(_currentState.ToString(), true);
    }

    public void SetSpeed(float speed) => _animator.SetFloat(AnimatorConstants.Speed, speed);
}

public enum UnitAnimations
{
    Idle,
    Walk,
    Run,
    Light,
    Quack,
    Landing,
    Death,
}