using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BaseTriggerButton : BaseButton
{
    [SerializeField] private UnityEvent<bool> _onValueChange;
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _deactive;
    [SerializeField] private bool _defaultButtonState;
    protected SavableBool _isActive;

    public override void OnStart(GameObject gameObject)
    {
        base.OnStart(gameObject);
        _isActive = new SavableBool(_fullButton.name, _defaultButtonState);
        SetButtonState(_isActive.Value);
    }

    public override void OnDisable() => _isActive.Save();

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        _isActive.Value = !_isActive.Value;
        _onValueChange?.Invoke(_isActive.Value);
    }

    protected override void OnButtonUp()
    {
        base.OnButtonUp();
        SetButtonState(_isActive.Value);
    }

    protected void SetButtonState(bool active) => _image.sprite = active ? _active : _deactive;
}