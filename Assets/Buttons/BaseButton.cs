using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class BaseButton : IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _buttonDown;
    [SerializeField] private UnityEvent _buttonUp;
    [SerializeField] private Sprite _whenPressed;
    [SerializeField] private Sprite _fullButton;
    [SerializeField] protected SimpleSound _buttonClick;
    private Image _image;
    protected AudioSource _audioSource;

    public event UnityAction ButtonDown
    {
        add => _buttonDown.AddListener(value);
        remove => _buttonDown.RemoveListener(value);
    }

    public event UnityAction ButtonUp
    {
        add => _buttonUp.AddListener(value);
        remove => _buttonUp.RemoveListener(value);
    }

    public virtual void OnStart() => _image.sprite = _fullButton;
    
    protected virtual bool IsActive() => true;

    protected virtual void OnButtonDown()
    {
        _image.sprite = _whenPressed;
        _audioSource.PlaySound(_buttonClick);
    }

    protected virtual void OnButtonUp() => _image.sprite = _fullButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsActive() == false) return;
        OnButtonDown();
        _buttonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsActive() == false) return;
        OnButtonUp();
        _buttonUp?.Invoke();
    }
}