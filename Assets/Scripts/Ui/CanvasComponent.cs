using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasComponent : MonoBehaviour
{
    [SerializeField] private GameObject _previousWindow;
    [SerializeField] private UnityEvent _previousWindowAction;
    [SerializeField] private NextWindow[] _nextWindow;
    [SerializeField] private AnimationClip _transitionClip;
    [SerializeField] private Animator _animator;
    private GameObject _currentWindow;
    private int _nextWindowCount;
        private void Start()
    {
        //_animator = GetComponentInChildren<Animator>();
        _currentWindow = gameObject;
    }

    public void OpenPreviousWindow()
    {
        _animator.Play(_transitionClip.name);
        Invoke(nameof(DoPreviousWindow),0.25f);
    }

    public void OpenNextWindow(int index)
    {
        _animator.Play(_transitionClip.name);
        _nextWindowCount = index;
        Invoke(nameof(DoNextWindow),0.25f);
    }
    
    public void DoNextWindow()
    {
        _nextWindow[_nextWindowCount].nextWindow.SetActive(true);
        Invoke(nameof(InvokeNextAction),0.3f);
        _currentWindow.SetActive(false);
    }

    private void InvokeNextAction()
    {
        _nextWindow[_nextWindowCount].nextWindowAction?.Invoke();
    }

    public void InvokePreviousAction()
    {
        _previousWindowAction?.Invoke();
    }

    private void DoPreviousWindow()
    {
        Invoke(nameof(InvokePreviousAction),0.3f);
        _previousWindow.SetActive(true);
        _currentWindow.SetActive(false);
    }
}
[Serializable]
public class NextWindow
{
    public GameObject nextWindow;
    public UnityEvent nextWindowAction;
}
