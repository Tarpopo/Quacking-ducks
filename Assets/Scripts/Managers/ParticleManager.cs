﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using Managers;
using UnityEngine;

public class ParticleManager : ManagerBase, ITick, IStart
{
    [SerializeField] private int _particleCount;
    [SerializeField] private GameObject _particles;

    private List<ParticleObject> _freeParticles = new List<ParticleObject>();

    private List<ParticleObject> _occupiedParticles = new List<ParticleObject>();
    // private static ParticleManager _particleManager;


    // public override void ClearScene()
    // {
    //     _freeParticles.Clear();
    //     _occupiedParticles.Clear();
    // }

    public void OnStart()
    {
        ManagerUpdate.AddTo(this);
        for (int i = 0; i < _particleCount; i++)
        {
            var particle = new ParticleObject();
            var obj = Instantiate(_particles);
            particle.obj = obj;
            particle.animator = obj.GetComponent<Animator>();
            particle.obj.SetActive(false);
            particle.transform = obj.transform;
            _freeParticles.Add(particle);
        }
    }

    public ParticleObject PlayDetachedParticle(AnimationClip clip, Vector3 position, float delay = 1,
        Transform transform = null, Action func = null)
    {
        var obj = _freeParticles[0];
        _freeParticles[0].func = func;
        _freeParticles[0].obj.SetActive(true);
        _freeParticles[0].animator.Play(clip.name);
        _freeParticles[0].transform.position = position;
        _freeParticles[0].time = delay;
        _freeParticles[0].transform.SetParent(transform);
        _occupiedParticles.Add(_freeParticles[0]);
        _freeParticles.RemoveAt(0);
        return obj;
    }

    // public void StopDetachedParticle(ParticleObject particleObject)
    // {
    //     //particleObject.func?.Invoke();
    //     particleObject.obj.SetActive(false);
    //     particleObject.transform.SetParent(null);
    //     particleObject.func = null;
    //     _freeParticles.Add(particleObject);
    //     _occupiedParticles.Remove(particleObject);
    // }
    public void PlayParticle(AnimationClip clip, Vector3 position, float delay = 1, Transform transform = null,
        Action func = null, int scale = 1)
    {
        _freeParticles[0].func = func;
        _freeParticles[0].obj.SetActive(true);
        _freeParticles[0].animator.Play(clip.name);
        _freeParticles[0].transform.position = position;
        _freeParticles[0].time = delay;
        _freeParticles[0].transform.localScale = scale == 1 ? Vector3.one : new Vector3(-1, 1, 0);
        _freeParticles[0].transform.SetParent(transform);
        _occupiedParticles.Add(_freeParticles[0]);
        _freeParticles.RemoveAt(0);
    }


    public void Tick()
    {
        for (int i = 0; i < _occupiedParticles.Count; i++)
        {
            _occupiedParticles[i].time -= Time.deltaTime;
            if (_occupiedParticles[i].time > 0) continue;
            _occupiedParticles[i].func?.Invoke();
            _occupiedParticles[i].obj.SetActive(false);
            _occupiedParticles[i].transform.SetParent(null);
            _occupiedParticles[i].func = null;
            _freeParticles.Add(_occupiedParticles[i]);
            _occupiedParticles.RemoveAt(i);
        }
    }
}

[Serializable]
public class ParticleObject
{
    public Action func;
    public Animator animator;
    public GameObject obj;
    public Transform transform;
    public float time;
}