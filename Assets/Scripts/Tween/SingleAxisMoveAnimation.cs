using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class SingleAxisMoveAnimation : BaseTweenAnimation
{
    [SerializeField] private Axis _axis;
    [SerializeField] private float _endValue;
    [SerializeField] private float _startValue;

    public override void OnStart()
    {
        base.OnStart();
        if (_startValue.Equals(0)) _startValue = _axis.GetAxisValue(_rectTransform.anchoredPosition3D);
    }

    public override void SetStartValues() => _rectTransform.anchoredPosition =
        _axis.AxisVector(_rectTransform.anchoredPosition, _startValue);

    public override void SetEndValues() =>
        _rectTransform.anchoredPosition = _axis.AxisVector(_rectTransform.anchoredPosition, _endValue);

    public override Tween GetTween()
    {
        var tween = _axis switch
        {
            Axis.X => XLocalMove(),
            Axis.Y => YLocalMove(),
            Axis.Z => ZLocalMove(),
            _ => XLocalMove()
        };

        return tween;
    }

    private Tween XLocalMove() => _rectTransform.DOLocalMoveX(_endValue, _duration);
    private Tween YLocalMove() => _rectTransform.DOLocalMoveY(_endValue, _duration);
    private Tween ZLocalMove() => _rectTransform.DOLocalMoveZ(_endValue, _duration);
}

public enum Axis
{
    X,
    Y,
    Z
}