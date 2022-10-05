using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

public class TilePlatformManager : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField]
    private GameObject RedTileMap;
    [SerializeField]
    private GameObject BlueTileMap;
    
    [Header("Settings")]
    [SerializeField]
    private bool isRedOn=true;
    
    //inner methods
    private PlayerInputActions playerInputActions;
    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        if (isRedOn)
        {
            TurnOffPlatforms(BlueTileMap);
        }
        else
        {
            TurnOffPlatforms(RedTileMap);
        }
    }
    private void GetInput()
    {
        if (playerInputActions.Player.RedPlatformToggle.IsPressed() && !isRedOn)
        {
            SoundManager.Instance.Play("PlatformOnSound");
            TogglePlatforms();
        }

        if (playerInputActions.Player.BluePlatformToggle.IsPressed() && isRedOn)
        {
            SoundManager.Instance.Play("PlatformOnSound");
            TogglePlatforms() ;
        }
    }
    private void TogglePlatforms()
    {
        isRedOn = !isRedOn;
        if (isRedOn)
        {
            TurnOffPlatforms(BlueTileMap);
            TurnOnPlatforms(RedTileMap);
        }
        else
        {
            TurnOffPlatforms(RedTileMap);
            TurnOnPlatforms(BlueTileMap);
        }
    }
    private void TurnOffPlatforms(GameObject tileMap)
    {
        tileMap.SetActive(false);
    }
    private void TurnOnPlatforms(GameObject tileMap)
    {
        tileMap.SetActive(true);
    }
    
    private void Update()
    {
        GetInput();
    }
}
