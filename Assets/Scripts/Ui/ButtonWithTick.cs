using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithTick : MonoBehaviour,ITick,IPointerDownHandler,IPointerUpHandler
{
    public Action ButtonUp;
    public Action ButtonDown;
    public Action ButtonUpdate;
    private float _currentUpdateTime;
    private SpriteRenderer _image;
    [SerializeField] private float _updateTime;
    [SerializeField] private Sprite _whenPressed;
    [SerializeField] private Sprite _fullButton;

    private void Start()
    {
        ManagerUpdate.AddTo(this);
        _image = GetComponent<SpriteRenderer>();
    }

    public void Tick()
    {
        if (_currentUpdateTime > 0)
        {
            ButtonUpdate();
            _currentUpdateTime -= Time.deltaTime;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _whenPressed;
        ButtonDown?.Invoke();
        _currentUpdateTime = _updateTime;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _fullButton;
        ButtonUp?.Invoke();
        _currentUpdateTime = 0;
    }
}
