using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Sound;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class UIController : MonoBehaviour
    {
        [Header("Menu Settings")] [SerializeField, Scene]
        private string sceneToLoad;
        
        [Header("Pause Settings")]
        [SerializeField]
        private bool paused;
        
        
        //inner methods
        private PlayerInputActions _playerInputActions;
        private bool _isDead;

        private void Awake()
        {
            _isDead = false;
            paused = false;
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            
            _playerInputActions.FindAction("Pause").started += OnPausePressed;
        }

        private void Start()
        {
            SoundManager.Instance.Play("Level1Music");
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
                    OnUnlockCursor();
                }
                paused = false;
            }
        }
        
        public void OnLockCursor()
        {
            MenuManager.Instance.CloseAllOpenMenus();
            //Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnUnlockCursor()
        {
            MenuManager.Instance.OpenMenu("PauseMenu");
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
    }
    
    
}

