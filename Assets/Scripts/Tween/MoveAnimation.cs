using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class MoveAnimation : BaseTweenAnimation
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _startPosition;

    public override void OnStart()
    {
        base.OnStart();
        if (_startPosition.Equals(Vector3.zero)) _startPosition = _rectTransform.anchoredPosition3D;
    }

    public override void SetStartValues() => _rectTransform.anchoredPosition = _startPosition;

    public override void SetEndValues() => _rectTransform.anchoredPosition = _endPosition;

    public override Tween GetTween() => _rectTransform.DOLocalMove(_endPosition, _duration);
}