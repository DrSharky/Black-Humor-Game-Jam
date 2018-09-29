using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField]
    private GameObject gameWinObject;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text memberCountText;
    [SerializeField]
    private Text countDownText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverPanel;

    private Action timeUpListener;
    private Action gameWinListener;
    private Action countDownStartListener;
    private bool dontTriggerTwice = true;

    void Awake()
    {
        gameWinObject.SetActive(false);
        gameWinListener = new Action(GameWin);
        countDownStartListener = new Action(CountDownStart);
    }

    void CountDownStart()
    {
        countDownText.transform.parent.gameObject.SetActive(true);
        timerText.transform.parent.gameObject.SetActive(true);
        memberCountText.transform.parent.gameObject.SetActive(true);
    }

    void GameWin()
    {
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        if (Mathf.Round(TimeManager.timer) > 0)
        {
            gameWinObject.GetComponent<Text>().text = "MAX FOLLOWERS!!!";
        }
        else
        {
            gameWinObject.GetComponent<Text>().text = "TIME UP!!!";
        }
        gameWinObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        //WAIT FOR ANIMATION TO PLAY
        gameOverPanel.SetActive(true);
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

        EventManager.StartListening("WinEvent", gameWinListener);
        EventManager.StartListening("TimeUp", gameWinListener);
        EventManager.StartListening("CountDownStart", countDownStartListener);
    }

    void Update()
    {
        memberCountText.text = "Followers: " + Recruit.cultMemberCount;
        timerText.text = Math.Round(TimeManager.timer, 1).ToString("0.0");

        if (Mathf.Round(TimeManager.countDownTime) == 0)
        {
            countDownText.text = "GO!";
        }
        else if (TimeManager.countDownTime > 0)
        {
            countDownText.text = Mathf.Round(TimeManager.countDownTime).ToString();
        }
        if (Mathf.Round(TimeManager.countDownTime) <= -1 && dontTriggerTwice)
        {
            countDownText.transform.parent.gameObject.SetActive(false);
            EventManager.TriggerEvent("PlayerMove");
            dontTriggerTwice = false;
        }
    }
}