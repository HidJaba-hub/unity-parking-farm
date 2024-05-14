using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusType : MonoBehaviour
{
    public GameObject countDown;
    public GameObject timer;
    private void Awake()
    {
        SaveLevelSystem.LoadLevel();
        countDown.SetActive(false);
        timer.SetActive(false);

        if(SaveLevelSystem.levelToSave.hasCountDown) countDown.SetActive(true);
        if(SaveLevelSystem.levelToSave.hasTimer) timer.SetActive(true);
    }
}
