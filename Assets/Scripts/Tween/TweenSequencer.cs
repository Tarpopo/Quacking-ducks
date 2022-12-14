using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TweenSequencer : MonoBehaviour
{
    [SerializeField] private Ease _ease = Ease.Linear;
    [SerializeReference] private BaseTweenAnimation[] _tweenDataAnimations;
    private Sequence _sequence;
    private bool _sequenceTriggered;

    [Button]
    public void PlayForward(Action onEnd = null)
    {
        _sequence.PlayForward();
        _sequence.onComplete = () => onEnd?.Invoke();
    }

    [Button]
    public void PlayBackward(Action onEnd = null)
    {
        _sequence.PlayBackwards();
        _sequence.onRewind = () => onEnd?.Invoke();
    }

    [Button]
    public void Rewind()
    {
        _sequence.Rewind();
    }

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
        foreach (var tween in _tweenDataAnimations) tween.OnStart();
    }

    public void OnEnable()
    {
        _sequence = DOTween.Sequence();
        BuildSequence();
    }

    public void OnDisable() => _sequence.Kill();

    private void BuildSequence()
    {
        _sequence.SetEase(_ease);
        _sequence.Pause();
        _sequence.SetAutoKill(false);
        foreach (var tween in _tweenDataAnimations)
        {
            if (tween.Join) _sequence.Join(tween.GetTween());
            else _sequence.Append(tween.GetTween());
        }
    }
}