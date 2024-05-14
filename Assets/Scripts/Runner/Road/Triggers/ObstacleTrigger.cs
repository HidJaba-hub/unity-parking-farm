using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleTrigger : PlayerTrigger
{
    public Collider target;
    public float maxOffsetToTarget;
    public UnityEvent winCondition;
    public UnityEvent looseCondition;
    
    private String _tagToTrigger = "Obstacle";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_tagToTrigger))
        {
            PlayerMove.StopForSec();
        }

        if (other.Equals(target))
        {
            winCondition?.Invoke();
        }
    }

    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (target.transform.position.z - PlayerMove.gameObject.transform.position.z > maxOffsetToTarget)
        {
            looseCondition?.Invoke();
        }
    }
}
