using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    //public GameObject levelEndObject;
    //public GameObject gameOverObject;

    //public string[] skeletonText = new string[4];

    //[SerializeField]
    //private GameObject dialoguePanel;
    //[SerializeField]
    //private GameObject dialogueText;
    //[SerializeField]
    //private GameObject playerHealth;
    [SerializeField]
    private Text memberCountText;

    //private string dialogueType;
    //private int dialogueIndex = 0;
    //private int speedMulti = 0;

	// Use this for initialization
	void Start()
	{
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

	}

    //public void DisplayDialoguePanel(string type)
    //{
    //    dialogueType = type;
    //    playerHealth.SetActive(false);
    //    dialoguePanel.SetActive(true);
    //}

    private void Update()
    {
        memberCountText.text = "Cult Members: " + Recruit.cultMemberCount;
    }

    //void NextDialogueLine(string nextLine)
    //{
    //    dialogueText.GetComponent<Text>().text = nextLine;
    //}

    //void ClearDialoguePanel()
    //{
    //    playerHealth.SetActive(true);
    //    dialoguePanel.SetActive(false);
    //}

    //public void OnLevelEnd()
    //{
    //    levelEndObject.SetActive(true);
    //}

    //public void OnGameOver()
    //{
    //    gameOverObject.SetActive(true);
    //}
}