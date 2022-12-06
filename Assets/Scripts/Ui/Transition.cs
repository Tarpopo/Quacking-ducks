using UnityEngine;
using UnityEngine.Events;

public class Transition : ManagerBase
{
    [SerializeField] private Animator _animator;
    // [SerializeField] private AudioSource _audioSource;
    // [SerializeField] private SimpleSound _door;

    private UnityAction _onClose;

    public void PlayCloseAnimation(UnityAction onClose)
    {
        _animator.PlayStateAnimation(TransitionAnimation.Close);
        _onClose += onClose;
    }

    public void PlayOpenAnimation() => _animator.PlayStateAnimation(TransitionAnimation.Open);

    public void DoTransitionAnimation(UnityAction onCloseAnimationEnd)
    {
        PlayCloseAnimation(() =>
        {
            onCloseAnimationEnd?.Invoke();
            PlayOpenAnimation();
        });
    }

    public void OnCloseEnd()
    {
        _onClose?.Invoke();
        _onClose = null;
    }

    // private void PlayDoorSound() => _audioSource.PlaySound(_door);
}