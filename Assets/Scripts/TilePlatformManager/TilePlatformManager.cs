using System;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;

public class TilePlatformManager : MonoBehaviour
{
    public static TilePlatformManager Instance;
    
    [Header("Dependencies")] 
    [SerializeField]
    private GameObject RedTileMap;
    [SerializeField]
    private GameObject BlueTileMap;
    
    [Header("Settings")]
    [SerializeField]
    private bool isRedOn=true;
    
    //inner methods
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //INPUT STUFF   
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.RedPlayer.Get().FindAction("PlatformToggle").started += RedPressedToggle;
        _playerInputActions.BluePlayer.Get().FindAction("PlatformToggle").started += BluePressedToggle;
    }

    private void Start()
    {
        
        if (isRedOn)
        {
            TurnOffPlatforms(BlueTileMap);
        }
        else
        {
            TurnOffPlatforms(RedTileMap);
        }
    }
    
    private void BluePressedToggle(InputAction.CallbackContext context)
    {
        if(isRedOn)
        {
            TogglePlatforms();
        }
    }
    private void RedPressedToggle(InputAction.CallbackContext context)
    {
        if (!isRedOn)
        {
            TogglePlatforms();
        }
    }
    
    private void TogglePlatforms()
    {
        SoundManager.Instance.Play("PlatformOnSound");
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

    private void OnEnable()
    {
        EnableToggle();
    }

    private void OnDisable()
    {
        DisableToggle();
    }

    public void DisableToggle()
    {
        _playerInputActions.RedPlayer.Disable();
        _playerInputActions.BluePlayer.Disable();
    }
    
    public void EnableToggle()
    {
        _playerInputActions.RedPlayer.Enable();
        _playerInputActions.BluePlayer.Enable();
    }
}
