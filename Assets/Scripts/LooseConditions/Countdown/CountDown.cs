using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : LooseCondition
{
    public int maxSteps;
    protected void Awake()
    {
        textToChange.text = maxSteps.ToString();
    }

    public override void LooseAction()
    {
        maxSteps--;
        textToChange.text = maxSteps.ToString();
        if (maxSteps == 0)
        {
            PauseAndShowBoard();
        }
    }
}
