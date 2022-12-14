using System;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class AudioPlayer : ManagerBase, IStart
{
    [SerializeField] private BackgroundMusic[] _backgroundMusics;
    [SerializeField] private AudioSource _simpleAudioSource;
    [SerializeField] private AudioSource _mainMusicSource;

    public void OnStart()
    {
        Toolbox.Get<LevelLoader>().OnLevelLoadedStart += SetBackgroundMusic;
    }

    public void PlaySound(SimpleSound sound) => _simpleAudioSource.PlayOneShot(sound);
    private void PlayMusic(SimpleSound music) => _mainMusicSource.PlaySound(music);

    private void SetBackgroundMusic(string sceneName)
    {
        foreach (var backgroundMusic in _backgroundMusics) backgroundMusic.TrySetMusic(PlayMusic, sceneName);
    }
}

[Serializable]
public class BackgroundMusic
{
    [SerializeField, Scene] private string _scene;
    [SerializeField] private SimpleSound _music;

    public void TrySetMusic(Action<SimpleSound> onEqualScene, string sceneName)
    {
        if (sceneName.Equals(_scene) == false) return;
        onEqualScene?.Invoke(_music);
    }
}