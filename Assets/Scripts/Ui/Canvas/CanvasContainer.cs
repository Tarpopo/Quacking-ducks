using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CanvasContainer : ManagerBase, IAwake
{
    [SerializeField] private BaseCanvas[] _baseCanvas;
    private Dictionary<Type, BaseCanvas> _canvas;

    public void OnAwake()
    {
        _canvas = new Dictionary<Type, BaseCanvas>();
        foreach (var canvas in _baseCanvas) Add(canvas);
        print(GetCanvas<GameUI>().Health.name);
    }

    public T GetCanvas<T>() where T : BaseCanvas
    {
        _canvas.TryGetValue(typeof(T), out var resolve);
        return (T)resolve;
    }

    private void Add(BaseCanvas canvas)
    {
        print(canvas.GetType());
        _canvas.Add(canvas.GetType(), canvas);
    }
}