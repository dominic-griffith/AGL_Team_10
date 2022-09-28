using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public delegate void OnTimerEnded();
    public event OnTimerEnded OnTimerEndedInfo;
        

    [Header("Dependencies")]
    [SerializeField] private TMP_Text timeCounter;
    //[SerializeField] private GameObject timerGameObject;
    //[SerializeField] private GameObject timesUpGameObject;

    
    [Header("Testing Zone")] 
    [SerializeField] private UnityEvent OnTimerEndedEvent;
    [SerializeField] private bool timerGoing;
    //[SerializeField] private bool startTimer;
    //[SerializeField] private bool timerEnded;
    [SerializeField] [Tooltip("Amount of time to count down")] [Range(0f,100f)] 
    private float timerTime;
    [SerializeField] private float elapsedTime;
    
    private TimeSpan _timePlaying;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {

        timeCounter.text = "Time: 00:00:00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = timerTime;//0f

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        elapsedTime = 0f;
        timeCounter.text = "Time: 00:00:00";
        Debug.Log("Called all subscribed methods");
        OnTimerEndedInfo?.Invoke();
    }
    private IEnumerator UpdateTimer()
    {
        while( timerGoing )
        {
            elapsedTime -= Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + _timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;
            
            if (elapsedTime <=0)
                EndTimer();

            yield return null;
        }
        
    }
}


