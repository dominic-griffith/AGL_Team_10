using UnityEngine;
using System;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [Header("Level Music")] 
        [SerializeField, Scene]
        private string menuSceneThemeSong;
        [SerializeField, Scene]
        private string gameplaySceneThemeSong;
        
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
        
        public void StopSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
    
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return;
            }
            s.source.Stop();
        }
        
        public void StopAllSound()
        {
            foreach (Sound s in sounds)
            {
                if (s == null)
                {
                    Debug.LogWarning("sound not found check spelling for: " + name);
                    return; 
                }
                s.source.Stop();
            }
        }
        
        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            StopAllSound();
            if (scene.name == menuSceneThemeSong)
            {
                Play("MenuMusic");
            }
            else if (scene.name == gameplaySceneThemeSong)
            {
                Play("Level1Music");
            }
        }
        
        public void ChangeVolumeAllOfType(float volumeAmount, TypeOfAudio audioType)
        {
            foreach (Sound s in sounds)
            {
                if (s == null)
                {
                    Debug.LogWarning("sound not found check spelling for: " + name);
                    return; 
                }
                
                if(s.audioType == audioType)
                    s.source.volume = volumeAmount;
            }
        }

        public float GetBackgroundVolumeLevel()
        {
            Sound s = Array.Find(sounds, sound => sound.audioType == TypeOfAudio.Background);
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return 0;
            }
            return s.source.volume;
        }
        
        public float GetSoundEffectsVolumeLevel()
        {
            Sound s = Array.Find(sounds, sound => sound.audioType == TypeOfAudio.SoundEffects);
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return 0;
            }
            return s.source.volume;
        }
    }
}

