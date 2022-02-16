using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlaySoundComponent : MonoBehaviour
{
   [SerializeField] private SimpleSound _sound;
   private AudioSource _audioSource;
   private void Start()
   {
      _audioSource = GetComponent<AudioSource>();
   }

   public void PlaySound()
   {
      _audioSource.PlayOneShot(_sound);
   }
}
