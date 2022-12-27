using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toolbox : Singleton<Toolbox>
{
    private readonly Dictionary<Type, ManagerBase> _data = new Dictionary<Type, ManagerBase>();
    private readonly List<IStart> _starts = new List<IStart>();

    private void Add(ManagerBase obj)
    {
        _data.Add(obj.GetType(), obj);
        if (obj is IAwake awake) awake.OnAwake();
        if (obj is IStart start) Instance._starts.Add(start);
    }

    public static T Get<T>() where T : ManagerBase
    {
        Instance._data.TryGetValue(typeof(T), out var resolve);
        return (T)resolve;
    }

    private void ClearScene(Scene scene) => _data.ForEach(data => data.Value.ClearScene());

    private void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach (var changed in _data.Select(obj => obj.Value as ISceneChanged)) changed?.OnChangeScene();
    }

    private void Awake()
    {
        foreach (var managerBase in Resources.FindObjectsOfTypeAll<ManagerBase>()) Add(managerBase);
        SceneManager.sceneLoaded += SceneChanged;
        SceneManager.sceneUnloaded += ClearScene;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneChanged;
        SceneManager.sceneUnloaded -= ClearScene;
    }

    private void Start() => _starts.ForEach(start => start.OnStart());
}