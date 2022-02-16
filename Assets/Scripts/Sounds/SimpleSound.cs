using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class SimpleSound
    { 
        public AudioClip audioClip; 
        [Range(0, 1)] public float volumeSound;
    }
}