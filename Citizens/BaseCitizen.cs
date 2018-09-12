using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class BaseCitizen : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private NavMeshAgent agent;

    private bool lookAtPlayer = false;
    private bool recruiting = false;
    private Vector3 playerPos;

    [SerializeField]
    private GameObject recruitDisplay;
    [SerializeField]
    private float recruitSpeed;
    private float recruitNum = 0.0f;
    private Text recruitText;

    private Action startRecruitListener;
    private Action stopRecruitListener;

    void Awake()
    {
        startRecruitListener = new Action(() => { recruiting = true; });
        stopRecruitListener = new Action(() => { recruiting = false; });
    }

    void Start()
    {
        EventManager.StartListening("StartRecruiting", startRecruitListener);
        EventManager.StartListening("StopRecruiting", stopRecruitListener);
        recruitText = recruitDisplay.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(lookAtPlayer)
        {
            playerPos = player.transform.position;
            playerPos.y = 2.0f;
            Vector3 newDir = playerPos - transform.position;
            Quaternion newRot = Quaternion.LookRotation(newDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, 7.0f);
            if(recruiting)
            {
                recruitNum = recruitNum + recruitSpeed * Time.deltaTime;
                recruitText.text = Mathf.Round(recruitNum) + "%";
            }
            if(recruitNum >= 100.0f)
            {
                EventManager.TriggerEvent("Convert" + gameObject.GetInstanceID());
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lookAtPlayer = true;
            agent.isStopped = true;
            EventManager.TriggerEvent("ToggleWander" + gameObject.GetInstanceID());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            lookAtPlayer = false;
            agent.isStopped = false;
            EventManager.TriggerEvent("ToggleWander" + gameObject.GetInstanceID());
        }
    }
}
