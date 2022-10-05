using UnityEngine;

namespace Sound
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;

        public TypeOfAudio audioType;

        [HideInInspector]
        public AudioSource source;
    }
    public enum TypeOfAudio
    {
        SoundEffects,
        Background
    }
}