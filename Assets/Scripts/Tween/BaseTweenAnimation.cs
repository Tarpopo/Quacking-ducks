using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public abstract class BaseTweenAnimation
{
    public bool Join => _join;
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected float _duration;
    [SerializeField] private bool _join;

    public abstract Tween GetTween();

    public virtual void OnStart()
    {
    }

    public abstract void SetStartValues();

    public abstract void SetEndValues();
}