using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class MoveButton : BaseButton
{
    public int MoveDirection { get; set; }
    [SerializeField] private Sprite _whenLeftPressed;
    [SerializeField] private Sprite _leftFullButton;
    [SerializeField] private Sprite _whenRightPressed;
    [SerializeField] private Sprite _rightFullButton;
    [SerializeField] private Image _leftButtonImage;
    [SerializeField] private Image _rightButtonImage;
    [SerializeField] private RectTransform _rectTransform;
    private Vector2 _position;
    private Camera _camera;

    public override void OnStart(GameObject gameObject)
    {
        _camera = Camera.main;
    }

    public override void OnDisable() => SetButtonsInactive();

    public override void OnPointerMove(PointerEventData eventData)
    {
        if (IsPressed == false) return;
        CheckButton(eventData);
    }

    protected override void OnButtonDown(PointerEventData eventData)
    {
        CheckButton(eventData);
    }

    protected override void OnButtonUp(PointerEventData eventData)
    {
        SetButtonsInactive();
    }

    private void SetButtonSpite(int direction)
    {
        MoveDirection = direction;
        if (MoveDirection >= 0)
        {
            _leftButtonImage.sprite = _leftFullButton;
            _rightButtonImage.sprite = _whenRightPressed;
        }
        else
        {
            _leftButtonImage.sprite = _whenLeftPressed;
            _rightButtonImage.sprite = _rightFullButton;
        }
    }

    private void SetButtonsInactive()
    {
        _leftButtonImage.sprite = _leftFullButton;
        _rightButtonImage.sprite = _rightFullButton;
    }

    private void CheckButton(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, _camera,
            out _position);
        SetButtonSpite(Math.Sign(_position.x));
    }
}