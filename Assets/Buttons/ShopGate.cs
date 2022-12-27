using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class ShopGate : LockButton
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _price;
    [SerializeField] private Ducks _duck;
    [SerializeField] private SimpleSound _openSound;
    private SavableBool _isUnlock;

    public override void OnStart(GameObject gameObject)
    {
        _isUnlock = new SavableBool(_duck.ToString());
        base.OnStart(gameObject);
    }

    public override void OnDisable() => _isUnlock.Save();

    protected override void TryActive()
    {
        if (_price <= 0 || _isUnlock.Value) ActiveButtonDown();
    }

    protected override bool IsActive() => _isUnlock.Value;

    protected override void ActiveButtonDown()
    {
        _audioPlayer.PlaySound(_openSound);
        _animator.PlayStateAnimation(LockAnimation.Open);
    }

    protected override void OnButtonUp(PointerEventData eventData)
    {
    }

    protected override void OnButtonDown(PointerEventData eventData)
    {
        if (_isUnlock.Value || Toolbox.Get<Shop>().TryReduceCoins(_price) == false)
        {
            _audioPlayer.PlaySound(_lockSound);
            return;
        }

        _isUnlock.Value = true;
        base.OnButtonDown(eventData);
    }
}