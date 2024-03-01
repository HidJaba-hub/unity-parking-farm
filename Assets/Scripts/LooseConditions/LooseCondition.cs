using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LooseCondition : MonoBehaviour
{
    public TextMeshProUGUI textToChange;
    public GameObject looseBoard;

    protected void PauseAndShowBoard()
    {
        Time.timeScale = 0;
        looseBoard.SetActive(true);
    }

    public virtual void LooseAction()
    {
    }
}
