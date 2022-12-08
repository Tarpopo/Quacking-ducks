using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ScaleAnimation : BaseTweenAnimation
{
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private Vector3 _startScale;

    public override void SetStartValues() => _rectTransform.localScale = _startScale;

    public override void SetEndValues() => _rectTransform.localScale = _endScale;

    public override Tween GetTween() => _rectTransform.DOScale(_endScale, _duration);
}