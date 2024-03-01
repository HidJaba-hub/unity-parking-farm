using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelEntry : MonoBehaviour
{
    public List<Move> movers = new List<Move>();
    public PlayListText playListText;

    public void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(playListText.AlphaCoroutine(() => {
            foreach (var move in movers)
            {
                move.Run();
            }
            playListText.gameObject.SetActive(false);
        }));
    }

    public void EndOfGame()
    {
        foreach (var move in movers)
        {
            move.StopRun();
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NextScene"));
    }

    public void LoadPrevScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PrevScene"));
    }
}
