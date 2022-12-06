using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TweenSequencer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAnimationEnd;
    [SerializeField] private UnityEvent _onRewindAnimationEnd;
    [SerializeReference] private BaseTweenAnimation[] _tweenDataAnimations;
    private Sequence _sequence;
    private bool _sequenceTriggered;

    [Button]
    public void PlayForward() => _sequence.PlayForward();

    [Button]
    public void PlayBackward() => _sequence.PlayBackwards();

    [Button]
    public void SetStartValues()
    {
        foreach (var tween in _tweenDataAnimations) tween.SetStartValues();
    }

    [Button]
    public void SetEndValues()
    {
        foreach (var tween in _tweenDataAnimations) tween.SetEndValues();
    }

    private void Awake()
    {
        _sequence = DOTween.Sequence();
        BuildSequence();
        foreach (var tween in _tweenDataAnimations) tween.OnStart();
    }

    private void BuildSequence()
    {
        _sequence.Pause();
        _sequence.SetAutoKill(false);
        foreach (var tween in _tweenDataAnimations)
        {
            if (tween.Join) _sequence.Join(tween.GetTween());
            else _sequence.Append(tween.GetTween());
        }

        _sequence.onComplete = OnComplete;
        _sequence.onRewind = OnRewindComplete;
    }

    private void OnComplete() => _onAnimationEnd?.Invoke();
    private void OnRewindComplete() => _onRewindAnimationEnd?.Invoke();
}