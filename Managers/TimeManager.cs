using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static float timer = 90.0f;
    public static float countDownTime = 3.0f;
    public static TimeManager instance = null;

    private bool countStart = false;
    private bool stopTime = false;
    private Action recruitListener;
    private Action winEventListener;
    private Action startCountdownListener;

    void Awake()
    {
        Time.timeScale = 0.0f;
        recruitListener = new Action(()=> { timer += 2.0f; });
        winEventListener = new Action(WinEvent);
        startCountdownListener = new Action(() => { Time.timeScale = 1.0f; });
    }

    void WinEvent()
    {
        stopTime = true;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        EventManager.StartListening("AddRecruit", recruitListener);
        EventManager.StartListening("WinEvent", winEventListener);
        EventManager.StartListening("CountDownStart", startCountdownListener);
    }

    void Update ()
    {
        if (!countStart)
        {
            countDownTime -= Time.deltaTime;
        }

        if(countStart && !stopTime)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            EventManager.TriggerEvent("TimeUp");
            stopTime = true;
        }

        if (countDownTime <= -1)
        {
            countStart = true;
        }
	}
}
