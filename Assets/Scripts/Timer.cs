using System;
using DefaultNamespace;
using UnityEngine;

public class Timer : MonoBehaviour, ITick
{
    private float _time;
    private bool _isEveryFrame;
    private Action _action;


    public void Tick()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            if (_isEveryFrame) _action?.Invoke();
            return;
        }

        _action?.Invoke();
        ResetTimer();
    }

    public bool GetIsTimerActive()
    {
        return _time > 0;
    }

    public void StartTimer(Action action, float time, bool isEveryFrame = false)
    {
        _time = time;
        _action = action;
        _isEveryFrame = isEveryFrame;
        ManagerUpdate.AddTo(this);
        //print("Im add timer "+_time);
    }

    private void ResetTimer()
    {
        _action = null;
        _time = 0;
        ManagerUpdate.RemoveFrom(this);
    }
}