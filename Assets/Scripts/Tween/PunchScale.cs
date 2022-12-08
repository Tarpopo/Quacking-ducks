using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class PunchScale : BaseTweenAnimation
{
    [SerializeField] private Vector3 _punch;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _elasticity;

    public override Tween GetTween() => _rectTransform.DOPunchScale(_punch, _duration, _vibrato, _elasticity);

    public override void SetStartValues()
    {
    }

    public override void SetEndValues()
    {
    }
}