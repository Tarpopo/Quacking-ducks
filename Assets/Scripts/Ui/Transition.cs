using System;
using DefaultNamespace;
using UnityEngine;

public class Transition : ManagerBase, IStart
{
    [SerializeField] private TweenSequencer _tweenSequencer;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SimpleSound _gateSound;
    private AudioPlayer _audioPlayer;

    public void OnStart() => _audioPlayer = Toolbox.Get<AudioPlayer>();

    public void PlayCloseAnimation(Action onAnimationEnd = null)
    {
        EnableCanvas();
        _audioPlayer.PlaySound(_gateSound);
        _tweenSequencer.PlayForward(onAnimationEnd);
    }

    public void PlayOpenAnimation(Action onAnimationEnd = null)
    {
        onAnimationEnd += DisableCanvas;
        _audioPlayer.PlaySound(_gateSound);
        _tweenSequencer.PlayBackward(onAnimationEnd);
    }

    private void EnableCanvas() => _canvas.gameObject.SetActive(true);
    private void DisableCanvas() => _canvas.gameObject.SetActive(false);
}