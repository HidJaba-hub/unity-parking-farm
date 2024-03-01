using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTrigger : PlayerTrigger
{
    public RoadActions roadActions;
    public void OnTriggerEnter(Collider other)
    {
        if (roadActions.RoadTriggered(other.gameObject))
        {
            PlayerMove.SpeedIncrease();
        }
    }
}
