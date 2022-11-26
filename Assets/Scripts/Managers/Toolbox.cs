using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine.SceneManagement;

public class Toolbox : Singleton<Toolbox>
{
    private readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();
    private readonly List<IStart> _starts = new List<IStart>();

    public void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach (var changed in _data.Select(obj => obj.Value as ISceneChanged))
        {
            changed?.OnChangeScene();
        }
    }

    public static void Add(object obj)
    {
        Instance._data.Add(obj.GetType(), obj);
        if (obj is IAwake awake) awake.OnAwake();
        if (obj is IStart start) Instance._starts.Add(start);
    }

    public static T Get<T>()
    {
        Instance._data.TryGetValue(typeof(T), out var resolve);
        return (T)resolve;
    }

    public void ClearScene(Scene scene)
    {
        foreach (var managerBase in _data.Select(obj => obj.Value).OfType<ManagerBase>())
        {
            managerBase.ClearScene();
        }
    }

    private void Start()
    {
        foreach (var start in _starts) start.OnStart();
    }
}