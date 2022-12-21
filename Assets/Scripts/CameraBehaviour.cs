using System;
using System.Data;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour, ITick
{
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [SerializeField] private float _time = 0.35f;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    private float _currentTime;

    private void Awake()
    {
        ManagerUpdate.AddTo(this);
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Tick()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0) StopShake();
        }
    }

    public void Shake(float amplitude, float freq)
    {
        virtualCameraNoise.m_AmplitudeGain = amplitude;
        virtualCameraNoise.m_FrequencyGain = freq;
        _currentTime = _time;
    }

    public void ShakeInAnimation()
    {
        virtualCameraNoise.m_AmplitudeGain = _amplitude;
        virtualCameraNoise.m_FrequencyGain = _frequency;
        _currentTime = _time;
    }

    private void StopShake()
    {
        virtualCameraNoise.m_AmplitudeGain = 0;
        virtualCameraNoise.m_FrequencyGain = 0;
    }
}