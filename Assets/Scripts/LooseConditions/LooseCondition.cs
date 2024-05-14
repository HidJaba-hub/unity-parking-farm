using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class LooseCondition : MonoBehaviour
{
    public TextMeshProUGUI textToChange;
    public GameObject looseBoard;

    protected void PauseAndShowBoard()
    {
        LevelEntry.Pause();
        looseBoard.SetActive(true);
    }

    public virtual void LooseAction()
    {
    }

    public virtual void SetPlusFiveBonuses()
    {
        
    }
}
