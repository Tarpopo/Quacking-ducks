using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerButton : MenuCustomButton
{
    [SerializeField] private UnityEvent _triggerChecker;
    [SerializeField] private Sprite _activeState;
    [SerializeField] private SimpleSound _lockSound;
    public bool _isTriggerActive;
    public bool IsActive;
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (IsActive == false)
        {
            _audioSource.PlaySound(_lockSound);
            return;
        }
        _audioSource.PlaySound(_buttonClick);
        ClickOnButton();
    }

    public void ClickOnButton()
    {
        _spriteRenderer.sprite = _whenPressed;
        if (_isTriggerActive == false)_triggerChecker?.Invoke();
        _isTriggerActive = !_isTriggerActive;
        ButtonDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(IsActive==false) return;
        _spriteRenderer.sprite = _isTriggerActive ? _activeState : _fullButton;
        ButtonUp?.Invoke();
    }

    public void SetActiveFull()
    {
        _spriteRenderer.sprite = _fullButton;
        _isTriggerActive = false;
    }
}
