using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public static class AudioManager 
{
    public static void PlaySound(this AudioSource _audio,SimpleSound simpleSound)
    {
        _audio.volume = simpleSound.volumeSound;
        _audio.clip = simpleSound.audioClip;
        _audio.Play();
    }
    public static void PlayOneShot(this AudioSource _audio,SimpleSound simpleSound)
    {
        _audio.PlayOneShot(simpleSound.audioClip,simpleSound.volumeSound);
    }

    public static T Random<T>(this List<T> list)
    { 
        return list[UnityEngine.Random.Range(0,list.Count)];
    }
    
    public static float AddExplosionForce(this Rigidbody2D rigidbody,float damage, float explForce,float damageRadius,
        Vector2 explPoint,ForceMode2D mode=ForceMode2D.Force)
    {
        var exploDirection = rigidbody.position - explPoint;
        var explDistance = exploDirection.magnitude;
        exploDirection.Normalize();
        var koof = Mathf.InverseLerp(0,damageRadius,explDistance);
        rigidbody.AddForce(Mathf.Lerp(explForce,0, koof ) * exploDirection,mode);
        return Mathf.Lerp(damage, 0, koof);
    }

}
// [Serializable]
// public struct AudioObject
// {
//     public Transform AudioTransform;
//     public AudioSource AudioSource;
//     public GameObject GameObject;
//     public float time;
// }
