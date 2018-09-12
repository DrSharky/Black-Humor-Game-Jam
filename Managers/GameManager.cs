using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameObject player = null;
    public static UIManager uiManager = null;

    public static UnityEvent LevelEnd;
    public static UnityEvent GameOver;

    private static int levelCount = 0;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (LevelEnd == null)
            LevelEnd = new UnityEvent();

        if (GameOver == null)
            GameOver = new UnityEvent();

        if (uiManager == null)
            uiManager = GetComponent<UIManager>();
    }

    private void AddLevelCount()
    {
        levelCount++;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}