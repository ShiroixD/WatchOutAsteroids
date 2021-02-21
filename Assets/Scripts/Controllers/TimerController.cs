using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private Text _text;
    float _startTime;

	void Start () {
        _text = GetComponent<Text>();
        _startTime = Time.time;
	}
	
	void Update () {
        UpdateTime();
	}

    private void UpdateTime()
    {
        float currentTime = Time.time - _startTime;
        float minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;

        _text.text = minutes.ToString() + ":" + seconds.ToString();
    }
}
