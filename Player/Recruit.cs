using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruit : MonoBehaviour
{
    bool holdingKey = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
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
        }

	}
}
