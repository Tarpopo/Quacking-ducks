using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelButton : MenuCustomButton
{
    [SerializeField] private Sprite _lock;
    [SerializeField] private GameObject _passedMark;
    [SerializeField] private Animator _animatorGate;
    [SerializeField] private SimpleSound _lockSound;
    public GameObject Gate;

    public AnimationClip UnlockClip;
    public AnimationClip UpButtonClip;
    public AnimationClip DownButtonClip;
    public AnimationClip FirstButtonState;
    public bool IsFirstOpen = true;
    public bool _isActive;


    public override void OnPointerDown(PointerEventData eventData)
    {
        //_spriteRenderer.sprite = _whenPressed;
        _audioSource.PlaySound(_isActive == false ? _lockSound : _buttonClick);
        _animatorGate.Play(DownButtonClip.name);
        ButtonDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_isActive == false) return;
        _audioSource.Stop();
        //_spriteRenderer.sprite = _fullButton;

        ButtonUp?.Invoke();
    }

    public void SetActiveFull()
    {
        _spriteRenderer.sprite = _fullButton;
        _isActive = false;
    }

    public void SetFirstState()
    {
        _animatorGate.Play(FirstButtonState.name);
    }

    public void UnlockDoor()
    {
        _animatorGate.Play(UnlockClip.name);
        _spriteRenderer.sprite = _lock;
    }

    public void PlayUpButtonAnimation()
    {
        Gate.SetActive(false);
        _animatorGate.Play(UpButtonClip.name);
    }


    public void ActivePassedMark()
    {
        _passedMark.SetActive(true);
    }
}