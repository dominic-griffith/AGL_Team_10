using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Sound;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class UIController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Slider backgroundVolumeSlider;
        [SerializeField]
        private Slider soundEffectsVolumeSlider;

        [SerializeField] private WinTileSystem winTileSystemScript;
        
        [Header("Menu Settings")] [SerializeField, Scene]
        private string sceneToLoad;
        
        [Header("Pause Settings")]
        [SerializeField]
        private bool paused;
        
        [Header("Event subscribers")][Tooltip("Add functions that will be called when timer reaches 0")] 
        [SerializeField] private UnityEvent OnGamePauseEvent;
        [SerializeField] private UnityEvent OnGameUnPauseEvent;
        
        
        //inner methods
        private PlayerInputActions _playerInputActions;
        private bool _isDead;

        private void Awake()
        {
            _isDead = false;
            paused = false;
            
            _playerInputActions = new PlayerInputActions();
            
            _playerInputActions.FindAction("Pause").started += OnPausePressed;
        }
        
        private void Start()
        {
            backgroundVolumeSlider.value = SoundManager.Instance.GetBackgroundVolumeLevel();
            soundEffectsVolumeSlider.value = SoundManager.Instance.GetSoundEffectsVolumeLevel();
        }

        public void Resume()
        {
            OnLockCursor();
        }

        public void Settings()
        {
            MenuManager.Instance.OpenMenu("SettingsMenu");
        }

        public void Back()
        {
            MenuManager.Instance.OpenMenu("PauseMenu");
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Update()
        {
            if (_isDead) return;
            
                PauseGame();
        }

        private void PauseGame()
        {
            if (paused)
            {
                if (MenuManager.Instance.IsMenuOpen("PauseMenu"))
                {
                    OnLockCursor();
                }
                else
                {
                    MenuManager.Instance.OpenMenu("PauseMenu");
                    OnUnlockCursor();
                }
                paused = false;
            }
        }
        
        public void OnLockCursor()
        {
            OnGameUnPauseEvent?.Invoke();
            MenuManager.Instance.CloseAllOpenMenus();
            //Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnUnlockCursor()
        {
            OnGamePauseEvent?.Invoke();
            //Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void OnPausePressed(InputAction.CallbackContext context)
        {
            paused = true;
        }

        public void SetPausedDead(bool isPlayerDead)
        {
            _isDead = isPlayerDead;
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();
        }
        
        public void OnChangedVolumeBackground()
        {
            SoundManager.Instance.ChangeVolumeAllOfType(backgroundVolumeSlider.value, TypeOfAudio.Background);
        }
        
        public void OnChangedVolumeSoundEffects()
        {
            SoundManager.Instance.ChangeVolumeAllOfType(soundEffectsVolumeSlider.value, TypeOfAudio.SoundEffects);
        }

        public void OnPlayerWon()
        {
            if (!winTileSystemScript.playerWon) return;
            
            SoundManager.Instance.StopAllSound();
            TimerManager.Instance.OnTimerPaused();
        }
    }
    
    
}

