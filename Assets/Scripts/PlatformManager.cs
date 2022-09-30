using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlatformManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private List<GameObject> RedPlatforms;
    public List<GameObject> BluePlatforms;
    public  bool isRedOn=true;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        RedPlatforms=new List<GameObject>(GameObject.FindGameObjectsWithTag("Red"));
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
    private void GetInput()
    {
        if (playerInputActions.Player.RedPlatformToggle.IsPressed()&&!isRedOn)
            TogglePlatforms();
        if (playerInputActions.Player.BluePlatformToggle.IsPressed() && isRedOn)
            TogglePlatforms() ;
    }
    private void TogglePlatforms()
    {
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
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
}
