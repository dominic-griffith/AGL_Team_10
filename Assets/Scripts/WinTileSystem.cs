using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Sound;
using UnityEngine;

public class WinTileSystem : MonoBehaviour
{
    [SerializeField]
    private UIController uiControllerScript;

    [SerializeField] public bool playerWon = false;
    //inner methods
    private bool _redPlayerReached =  false;
    private bool _bluePlayerReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && !_redPlayerReached)
        {
            playerWon = true;
            _redPlayerReached = true;
            uiControllerScript.OnUnlockCursor();
            SoundManager.Instance.Play("WinSound");
            MenuManager.Instance.OpenMenu("WinMenu");
            
        }
    }
}
