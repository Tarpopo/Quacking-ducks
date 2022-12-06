using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class BaseButton
{
    [SerializeField] private UnityEvent _buttonDown;
    [SerializeField] private UnityEvent _buttonUp;
    [SerializeField] private Sprite _whenPressed;
    [SerializeField] protected Sprite _fullButton;
    [SerializeField] protected SimpleSound _buttonClick;

    protected Image _image;
    // protected AudioSource _audioSource;

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

    public virtual void OnStart(GameObject gameObject)
    {
        _image = gameObject.GetComponent<Image>();
        if (_fullButton != null) _image.sprite = _fullButton;
    }

    public virtual void OnDisable() => _image.sprite = _fullButton;

    public virtual void OnDestroy()
    {
    }

    protected virtual void OnButtonDown()
    {
        if (_image == null) return;
        _image.sprite = _whenPressed;
        // _audioSource.PlaySound(_buttonClick);
    }

    protected virtual void OnButtonUp()
    {
        if (_image == null) return;
        _image.sprite = _fullButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonDown();
        _buttonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonUp();
        _buttonUp?.Invoke();
    }
}