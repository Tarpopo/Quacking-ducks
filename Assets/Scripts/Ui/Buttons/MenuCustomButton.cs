using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class MenuCustomButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public UnityEvent ButtonDown;
    public UnityEvent ButtonUp;

    [SerializeField] protected SimpleSound _buttonClick;
    [SerializeField] protected Sprite _whenPressed;
    [SerializeField] protected Sprite _fullButton;

    protected AudioSource _audioSource;
    protected SpriteRenderer _spriteRenderer;
    
    public void Start()
    {
        _audioSource=GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        print("ButtonDown");
        _spriteRenderer.sprite = _whenPressed;
        _audioSource.PlaySound(_buttonClick);
        ButtonDown?.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _audioSource.Stop();
        _spriteRenderer.sprite = _fullButton;
        ButtonUp?.Invoke();
    }
   
}
