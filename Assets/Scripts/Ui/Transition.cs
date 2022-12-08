using System;
using UnityEngine;
using UnityEngine.Events;

public class Transition : ManagerBase
{
    [SerializeField] private Animator _animator;

    // [SerializeField] private AudioSource _audioSource;
    // [SerializeField] private SimpleSound _door;
    public void PlayCloseAnimation(Action onAnimationEnd = null) =>
        _animator.PlayStateAnimation(TransitionAnimation.Close).AddOnComplete(onAnimationEnd);

    public void PlayOpenAnimation(Action onAnimationEnd = null) =>
        _animator.PlayStateAnimation(TransitionAnimation.Open).AddOnComplete(onAnimationEnd);

    public void DoTransitionAnimation(UnityAction onCloseAnimationEnd)
    {
        // PlayCloseAnimation(() =>
        // {
        //     onCloseAnimationEnd?.Invoke();
        //     PlayOpenAnimation();
        // });
    }

    // public void OnCloseEnd()
    // {
    //     _onClose?.Invoke();
    //     _onClose = null;
    // }

    // private void PlayDoorSound() => _audioSource.PlaySound(_door);
}