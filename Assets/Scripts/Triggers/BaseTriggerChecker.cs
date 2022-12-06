using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class BaseTriggerChecker
{
    [SerializeField] private UnityEvent _onObjectEnter = new UnityEvent();
    [SerializeField] private UnityEvent _onObjectExit = new UnityEvent();

    public event UnityAction OnGetObject
    {
        add => _onObjectEnter.AddListener(value);
        remove => _onObjectEnter.RemoveListener(value);
    }

    public event UnityAction OnLostObject
    {
        add => _onObjectExit.AddListener(value);
        remove => _onObjectExit.RemoveListener(value);
    }

    protected abstract bool IsThisObject(GameObject gameObject);

    protected virtual void OnEnter(GameObject enterObject)
    {
    }

    protected virtual void OnExit(GameObject exitObject)
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var gameObject = other.gameObject;
        if (IsThisObject(gameObject) == false) return;
        OnEnter(other.gameObject);
        _onObjectEnter?.Invoke();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        var gameObject = other.gameObject;
        if (IsThisObject(gameObject) == false) return;
        OnExit(other.gameObject);
        _onObjectExit?.Invoke();
    }
}