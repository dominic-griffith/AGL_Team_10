using System;
using NaughtyAttributes;
using Sound;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Slider backgroundVolumeSlider;
        [SerializeField]
        private Slider soundEffectsVolumeSlider;
        
        [Header("Settings")] [SerializeField, Scene]
        private string sceneToLoad;

        private void Start()
        {
            backgroundVolumeSlider.value = SoundManager.Instance.GetBackgroundVolumeLevel();
            soundEffectsVolumeSlider.value = SoundManager.Instance.GetSoundEffectsVolumeLevel();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void About()
        {
            MenuManager.Instance.OpenMenu("AboutMenu");
        }
        
        public void Settings()
        {
            MenuManager.Instance.OpenMenu("SettingsMenu");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void Back()
        {
            MenuManager.Instance.OpenMenu("MainMenu");
        }

        public void OnChangedVolumeBackground()
        {
            SoundManager.Instance.ChangeVolumeAllOfType(backgroundVolumeSlider.value, TypeOfAudio.Background);
        }
        
        public void OnChangedVolumeSoundEffects()
        {
            SoundManager.Instance.ChangeVolumeAllOfType(soundEffectsVolumeSlider.value, TypeOfAudio.SoundEffects);
        }
        
    }
}

