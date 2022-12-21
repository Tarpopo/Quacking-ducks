using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour, IUnityAdsListener
{
    private const string _adsId = "4002259";
    public SimpleSound QuackSound;
    [SerializeField] private Actor _player;
    private AudioSource _audioSource;
    private GameObject _menu;
    private GameObject _deathName;
    private int _count;
    private PropeSpawner _propeSpawner;

    private void Start()
    {
        Advertisement.Initialize(_adsId, false);
        _propeSpawner = FindObjectOfType<PropeSpawner>();
        _player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _menu = GameObject.FindWithTag("LevelsMenu");
        _deathName = GameObject.FindWithTag("DeathScreen");
    }

    private void OnEnable()
    {
        Advertisement.AddListener(this);
    }

    private void OnDisable()
    {
        Advertisement.RemoveListener(this);
    }

    public void ShowAd()
    {
        if (_count >= 2)
        {
            _audioSource.PlaySound(QuackSound);
            return;
        }

        _count++;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (Toolbox.Get<SceneController>().GetIsMainScene()) return;
        Toolbox.Get<EndLevelChecker>().UnlockScreen();
        _propeSpawner?.CheckAllProps();
        _player.MakeActorAlive();
        _menu.SetActive(false);
        _deathName.SetActive(false);
    }
}