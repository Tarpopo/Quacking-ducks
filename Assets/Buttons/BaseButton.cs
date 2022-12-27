using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class BaseButton
{
    public bool IsPressed { get; private set; }
    [SerializeField] private UnityEvent _buttonDown;
    [SerializeField] private UnityEvent _buttonUp;
    [SerializeField] private Sprite _whenPressed;
    [SerializeField] protected Sprite _fullButton;
    [SerializeField] protected SimpleSound _buttonClick;
    protected AudioPlayer _audioPlayer;
    protected Image _image;
    private int _pointerId;

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
        _audioPlayer = Toolbox.Get<AudioPlayer>();
        if (_fullButton != null) _image.sprite = _fullButton;
    }

    public virtual void OnDisable() => _image.sprite = _fullButton;

    public virtual void OnDestroy()
    {
    }

    protected virtual void OnButtonDown(PointerEventData eventData = null)
    {
        if (_image == null) return;
        _audioPlayer.PlaySound(_buttonClick);
        _image.sprite = _whenPressed;
    }

    protected virtual void OnButtonUp(PointerEventData eventData = null)
    {
        if (_image == null) return;
        _image.sprite = _fullButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerId = eventData.pointerId;
        IsPressed = true;
        OnButtonDown(eventData);
        _buttonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_pointerId != eventData.pointerId) return;
        IsPressed = false;
        OnButtonUp(eventData);
        _buttonUp?.Invoke();
    }

    public virtual void OnPointerMove(PointerEventData eventData)
    {
    }
}