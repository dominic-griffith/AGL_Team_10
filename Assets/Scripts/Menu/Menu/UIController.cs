using System;
using NaughtyAttributes;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class UIController : MonoBehaviour
    {
        [Header("Settings")] [SerializeField, Scene]
        private string sceneToLoad;
    
        public void StartGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void About()
        {
            MenuManager.Instance.OpenMenu("AboutMenu");
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
        
    }
}

