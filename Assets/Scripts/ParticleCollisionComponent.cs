﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ParticleCollisionComponent : MonoBehaviour
{
   private Transform _transform;
   private int _damage;
   private float _force;
   private IDamagable _damagableObj;
   private Loader _loader;
   private ParticleSystem _particleSystem;
   private void Start()
   {
      _particleSystem=GetComponent<ParticleSystem>();
   }

   public void SetParameters(Transform playPosition, int damage, float force)
   {
      _transform = playPosition;
      _damage = damage;
      _force = force;
   }

   public void Play(Loader loader)
   {
      _loader = loader;
      gameObject.transform.position = _transform.position;
      _particleSystem.Play();
   }

   private void OnParticleCollision(GameObject other)
   {
      if (_loader.damagableObjects.TryGetValue(other, out _damagableObj))
      {
         _damagableObj.ApplyDamage(_damage,_transform.position,_force);
      }
   }
}
