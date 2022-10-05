using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlatformManager : MonoBehaviour
{
    private List<GameObject> RedPlatforms;
    private List<GameObject> BluePlatforms;
    public  bool isRedOn=true;

    public static PlatformManager Instance;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        RedPlatforms =new List<GameObject>(GameObject.FindGameObjectsWithTag("Red"));
        BluePlatforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blue"));
        if (isRedOn)
        {
            TurnOffPlatforms(BluePlatforms);
        }
        else
        {
            TurnOffPlatforms(RedPlatforms);
        }
    }
    public void BluePressedToggle()
    {
        if(isRedOn)
        {
            TogglePlatforms();
        }
    }
    public void RedPressedToggle()
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
            TurnOffPlatforms(BluePlatforms);
            TurnOnPlatforms(RedPlatforms);
        }
        else
        {
            TurnOffPlatforms(RedPlatforms);
            TurnOnPlatforms(BluePlatforms);
        }
    }
    private void TurnOffPlatforms(List<GameObject> Platforms)
    {
        foreach(GameObject platform in Platforms)
        {
            platform.SetActive(false);
        }
    }
    private void TurnOnPlatforms(List<GameObject> Platforms)
    {
        foreach (GameObject platform in Platforms)
        {
            platform.SetActive(true);
        }
    }
}
