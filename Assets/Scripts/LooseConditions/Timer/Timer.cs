using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : LooseCondition
{
    [Header("time in seconds")]
    public int timeToPlay = 100;

    private bool isActive = false;
    
    protected void Awake()
    {
        SecondToMinutes(timeToPlay);
    }
    public override void LooseAction()
    {
        if(isActive) return;
        isActive = true;
        StartCoroutine (TimerCountdown(timeToPlay));
    }

    private IEnumerator TimerCountdown(float time)
    {
        while(time>0)
        {
            time -= 1f;
            if (time < 0) time = 0;
            
            SecondToMinutes(time);
            
            yield return new WaitForSeconds(1);
        }

        PauseAndShowBoard();
    }

    private void SecondToMinutes(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        var timeInMinutes = minutes + ":" + seconds;
        textToChange.text = timeInMinutes;
    }
}
