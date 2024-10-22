using System;
using UnityEngine;

namespace CulTA
{
    public class MonoAudioPlayer : MonoBehaviour
    {
        private static MonoAudioPlayer global;
        private AudioSource _audioSource;

        private void Awake()
        {
            global = this; // use the arbitrary instance.
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public static void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            if (!global) throw new InvalidOperationException();
            global._audioSource.PlayOneShot(clip, volume);
        }
    }
}