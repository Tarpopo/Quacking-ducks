using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Loader _loader;
    private IDamagable _damagable;
    void Start()
    {
        _loader = Toolbox.Get<Loader>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _loader.damagableObjects.TryGetValue(other.gameObject,out _damagable);
        _damagable?.ApplyDamage(100,Vector2.zero,0);
    }
}
