using System;
using UnityEngine;

[Serializable]
public class ShopGate : LockButton
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _price;
    [SerializeField] private Ducks _duck;
    private SavableBool _isUnlock;

    public override void OnStart(GameObject gameObject)
    {
        _isUnlock = new SavableBool(_duck.ToString());
        base.OnStart(gameObject);
    }

    public override void OnDisable() => _isUnlock.Save();

    protected override void TryActive()
    {
        if (_price <= 0 || _isUnlock.Value) _animator.PlayStateAnimation(LockAnimation.Open);
    }

    protected override bool IsActive() => _isUnlock.Value;

    protected override void ActiveButtonDown()
    {
        _animator.PlayStateAnimation(LockAnimation.Open);
    }

    protected override void OnButtonUp()
    {
    }

    protected override void OnButtonDown()
    {
        if (_isUnlock.Value || Toolbox.Get<Shop>().TryReduceCoins(_price) == false) return;
        _isUnlock.Value = true;
        base.OnButtonDown();
    }
}