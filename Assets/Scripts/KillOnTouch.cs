using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundManager.Instance.Play("ObstacleDeathSound");
            DeathManager.restartLevel();
        }
    }
}
