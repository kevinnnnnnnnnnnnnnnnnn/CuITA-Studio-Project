using System;
using UnityEngine;

namespace CulTA
{
    [CreateAssetMenu(fileName = "BuiltInResources", menuName = "CulTA/BuiltInResources")]
    public class BuiltInResources : ScriptableObject
    {
        public SoundSample[] soundSamples;

        public AudioClip GetSampleByName(string key)
        {
            foreach (var soundSample in soundSamples)
            {
                if (soundSample.key == key)
                {
                    return soundSample.sample;
                }
            }

            return null;
        }

        [Serializable]
        public class SoundSample
        {
            public string key;
            public AudioClip sample;
        }
    }
}
