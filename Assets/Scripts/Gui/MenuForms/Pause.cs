using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseBoard;
    
    public void SetPauseAndShowBoard()
    {
        pauseBoard.SetActive(true);
    }
}
