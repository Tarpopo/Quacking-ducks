using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerCollector<T> : BaseTriggerChecker
{
    public IEnumerable<T> Elements => _elements.Values;
    private readonly Dictionary<int, T> _elements = new Dictionary<int, T>(5);
    protected override bool IsThisObject(GameObject gameObject) => false;

    protected override void OnEnter(GameObject gameObject) =>
        _elements.Add(gameObject.GetInstanceID(), GetComponent(gameObject));

    protected override void OnExit(GameObject gameObject) => _elements.Remove(gameObject.GetInstanceID());

    protected abstract T GetComponent(GameObject gameObject);
}