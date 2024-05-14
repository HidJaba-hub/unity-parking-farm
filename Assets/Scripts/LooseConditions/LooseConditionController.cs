using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseConditionController : MonoBehaviour
{
    public LooseCondition looseCondition;

    public void StartCondition()
    {
        looseCondition.LooseAction();
    }

    public void SetBonuses()
    {
        SaveLevelSystem.levelToSave.AddLooseCondition(looseCondition);
        if(SaveLevelSystem.levelToSave.IsBonused) looseCondition.SetPlusFiveBonuses();
    }
}
