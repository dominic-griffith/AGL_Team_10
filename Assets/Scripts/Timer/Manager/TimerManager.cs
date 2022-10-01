using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
        

    [Header("Dependencies")]
    [SerializeField] private TMP_Text timeCounter;

    [Header("Timer Settings")]
    [SerializeField] private bool timerGoing;
    [SerializeField] [Tooltip("Amount of time to count down")] [Range(0f,100f)] 
    private float timerTime;
    [SerializeField] private float elapsedTime;
    
    [Header("Timer text color settings")]
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    [SerializeField] [Tooltip("Amount of time until colors start flashing")] [Range(0f,100f)] 
    private float warningColorsOnSecond;
    [SerializeField] [Range(0f, 20f)] 
    private float lerpColorsSpeed;
    
    [Header("Timer text font settings")]
    [SerializeField] private float firstFontSize;
    [SerializeField] private float secondFontSize;
    [SerializeField] [Range(0f, 20f)] 
    private float lerpFontSizeSpeed;
    
    [Header("Event subscribers")][Tooltip("Add functions that will be called when timer reaches 0")] 
    [SerializeField] private UnityEvent OnTimerEndedEvent;
    
    //inner variables
    private TimeSpan _timePlaying;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        timeCounter.text = "00:00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = timerTime;
        StartCoroutine(UpdateTimer());
    }

    private void EndTimer()
    {
        timerGoing = false;
        elapsedTime = 0f;
        timeCounter.text = "00:00";
        timeCounter.color = firstColor;
        OnTimerEndedEvent?.Invoke();
    }
    private IEnumerator UpdateTimer()
    {
        while( timerGoing )
        {
            elapsedTime -= Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr =  _timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;
            
            if (elapsedTime <=0)
                EndTimer();

            yield return null;
        }
        
    }

    private void LerpColors()
    {
        timeCounter.color = Color.Lerp(firstColor, secondColor,Mathf.Sin(Time.time* lerpColorsSpeed) );
    }

    private void LerpSize()
    {
        timeCounter.fontSize = Mathf.Lerp(firstFontSize, secondFontSize, Mathf.Sin(Time.time * lerpFontSizeSpeed));
    }

    private void Update()
    {
        if (elapsedTime <= warningColorsOnSecond && timerGoing)
        {
             LerpColors();
             LerpSize();
        }
    }
}


