using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeScalerComponent : MonoBehaviour
{
    private UnityAction _onScaleChanged;

    public event UnityAction OnScaleChanged
    {
        add => _onScaleChanged += value;
        remove => _onScaleChanged -= value;
    }

    public void UpdateTimeScaler()
    {
        print("time scale update");
        _onScaleChanged?.Invoke();
    }

    private void Start()
    {
        Toolbox.Get<TimeScaler>().AddScalerComponent(this);
    }
}