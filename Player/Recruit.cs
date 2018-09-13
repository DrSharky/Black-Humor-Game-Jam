using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Recruit : MonoBehaviour
{
    public static float cultMemberCount = 0;
    public static float recruitMultiplier = 1.0f;

    private bool holdingKey = false;
    private Action recruitListener;

    void Awake()
    {
        recruitListener = new Action(AddRecruit);
    }

    void Start ()
    {
        EventManager.StartListening("AddRecruit", recruitListener);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holdingKey)
        {
            EventManager.TriggerEvent("StartRecruiting");
            holdingKey = true;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            EventManager.TriggerEvent("StopRecruiting");
            holdingKey = false;
        }

	}

    void AddRecruit()
    {
        cultMemberCount++;
        recruitMultiplier += 0.15f;
    }
}
