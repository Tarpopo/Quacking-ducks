using System;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;

[Serializable]
public class AnimationComponent
{
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private Animator _animator;
    [SerializeReference] private Enum _animations;
    private Enum _currentState = null;

    [Button]
    private void UpdateAnimator()
    {
        _animatorController.GenerateAnimatorControllerFromExist(_animations, true);
    }

    public void PlayAnimation(Enum animationType)
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