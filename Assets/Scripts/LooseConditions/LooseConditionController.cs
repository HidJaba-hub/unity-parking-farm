using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseConditionController : MonoBehaviour
{
    public LooseCondition looseCondition;

    public void StartCondition()
    {
        looseCondition.LooseAction();
    }
}
