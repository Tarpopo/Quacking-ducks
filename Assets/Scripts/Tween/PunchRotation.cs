using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class PunchRotation : BaseTweenAnimation
{
    [SerializeField] private Vector3 _punchRotation;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _elasticity;

    public override Tween GetTween() =>
        _rectTransform.DOPunchRotation(_punchRotation, _duration, _vibrato, _elasticity);

    public override void SetStartValues()
    {
    }

    public override void SetEndValues()
    {
    }
}