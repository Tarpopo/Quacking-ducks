using DefaultNamespace;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSetter : ManagerBase, IStart
{
    [SerializeField] private AudioMixer _audioMixer;
    private string[] _exposeParameters;

    private SavableBool _isActive;

    public void SetSound(bool active)
    {
        var volume = active ? 0 : -80;
        foreach (var mixer in _exposeParameters) _audioMixer.SetFloat(mixer, volume);
        _isActive.Value = active;
        _isActive.Save();
    }

    public void OnStart()
    {
        _isActive = new SavableBool(nameof(AudioSetter), true);
        _exposeParameters = _audioMixer.GetExposedParameters();
        SetSound(_isActive.Value);
    }
}