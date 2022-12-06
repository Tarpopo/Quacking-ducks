using DefaultNamespace;
using UnityEngine;

public static class AudioSourceExtesions
{
    public static void PlaySound(this AudioSource audioSource, SimpleSound simpleSound)
    {
        audioSource.volume = simpleSound.volumeSound;
        audioSource.clip = simpleSound.audioClip;
        audioSource.Play();
    }

    public static void PlayOneShot(this AudioSource audioSource, SimpleSound simpleSound) =>
        audioSource.PlayOneShot(simpleSound.audioClip, simpleSound.volumeSound);
}