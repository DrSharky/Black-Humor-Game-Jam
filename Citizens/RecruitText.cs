using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitText : MonoBehaviour
{
    Text recText;
    void Start()
    {
        recText = GetComponentInChildren<Text>();
        recText.text = "";
    }

    void Update ()
    {
        Vector3 posAdjust = new Vector3(0.0f, 3.4f, 0.0f);
        Vector3 test = Camera.main.WorldToScreenPoint(transform.parent.position + posAdjust);
        recText.transform.position = test;
	}
}
