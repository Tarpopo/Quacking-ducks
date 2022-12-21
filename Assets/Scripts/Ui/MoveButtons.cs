using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MoveButtons : MonoBehaviour, ITick, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent<int> MoveAction;
    public UnityEvent ButtonUp;
    public UnityEvent ButtonDown;
    private float _midPosition;
    private bool _isAction;
    [SerializeField] private SpriteRenderer _leftImage;
    [SerializeField] private SpriteRenderer _rightImage;
    [SerializeField] private Sprite _interactLeft;
    [SerializeField] private Sprite _baseLeft;
    [SerializeField] private Sprite _interactRight;
    [SerializeField] private Sprite _baseRight;
    private float _screenWidth;

    private void Start()
    {
        ManagerUpdate.AddTo(this);
        _leftImage.sprite = _baseLeft;
        _rightImage.sprite = _baseRight;
        _midPosition = Camera.main.WorldToScreenPoint(_leftImage.transform.parent.position).x;
        _screenWidth = Screen.width / 2;
    }

    public void Tick()
    {
        if (Input.touches.Length > 0 && _isAction)
        {
            var input = Input.GetTouch(0);
            if (Input.touches.Length > 1)
            {
                if (input.position.x > _screenWidth) input = Input.GetTouch(1);
            }

            if (input.position.x >= _midPosition)
            {
                MoveAction.Invoke(1);
                _rightImage.sprite = _interactRight;
                _leftImage.sprite = _baseLeft;
            }
            else
            {
                MoveAction.Invoke(-1);
                _leftImage.sprite = _interactLeft;
                _rightImage.sprite = _baseRight;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isAction = true;
        ButtonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isAction = false;
        _leftImage.sprite = _baseLeft;
        _rightImage.sprite = _baseRight;
        ButtonUp?.Invoke();
    }
}