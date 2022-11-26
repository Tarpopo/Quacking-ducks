using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : ManagerBase
{
    [SerializeField] private float _timeScale;
    private List<TimeScalerComponent> _components = new List<TimeScalerComponent>();

    public void AddScalerComponent(TimeScalerComponent component)
    {
        _components.Add(component);
    }

    public void SetTimeScale()
    {
        Time.fixedDeltaTime = _timeScale * Time.fixedDeltaTime;
        Time.timeScale = _timeScale;
        foreach (var scaler in _components)
        {
            scaler.UpdateTimeScaler();
        }
    }
}