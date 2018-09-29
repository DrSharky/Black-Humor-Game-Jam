using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameObject player = null;
    public static UIManager uiManager = null;

    private int citizensRemaining;
    private Action addRecruitListener;
    private bool populateWait = false;

    void Awake()
    {
        Application.targetFrameRate = 60;
        addRecruitListener = new Action(() => { citizensRemaining--; });
    }

    // Use this for initialization
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

        if (uiManager == null)
        {
            uiManager = GetComponent<UIManager>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        EventManager.StartListening("AddRecruit", addRecruitListener);

        StartCoroutine(WaitForSpawn());
        EventManager.TriggerEvent("StartCountdown");
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject[] citizens = GameObject.FindGameObjectsWithTag("Citizen");
        populateWait = true;
        citizensRemaining = citizens.Length;
    }

    // Update is called once per frame
    void Update()
	{
        if (citizensRemaining == 0 && populateWait)
        {
            EventManager.TriggerEvent("WinEvent");
            citizensRemaining = -1;
        }
	}
}