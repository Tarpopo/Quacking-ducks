using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = nameof(SimpleSound), menuName = "Data/SimpleSound")]
    public class SimpleSound : ScriptableObject
    {
        public AudioClip audioClip;
        [Range(0, 1)] public float volumeSound = 0.7f;
    }
}