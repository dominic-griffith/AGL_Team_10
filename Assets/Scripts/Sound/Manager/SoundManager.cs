using UnityEngine;
using System;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        
        [SerializeField]
        private Sound[] sounds;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
    
            //DontDestroyOnLoad(gameObject);
    
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
    
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
        private void Start()
        {
            PlayMusic();
        }

        private void PlayMusic()
        {
            Play("Level1Music");
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
    
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return;
            }
    
            s.source.Play();
        }
    
        public void PauseSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
    
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return;
            }
            s.source.Pause();
        }
    }
}

