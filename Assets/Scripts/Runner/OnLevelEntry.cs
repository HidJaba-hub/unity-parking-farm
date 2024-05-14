using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelEntry : MonoBehaviour
{
    public List<Move> movers = new List<Move>();
    public PlayListText playListText;

    public void Start()
    {
        Time.timeScale = 1;
        playListText.AlphaTextAnimation(0, () => {
            foreach (var move in movers)
            {
                move.Run();
            }
            playListText.gameObject.SetActive(false); }
            );
    }

    public void EndOfGame()
    {
        foreach (var move in movers)
        {
            move.StopRun();
        }
    }
    public void LoadPrevSceneUnBonused()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("PrevScene"));
    }

    public void LoadPrevScene()
    {
        int prevScene = PlayerPrefs.GetInt("PrevScene");
        SaveLevelSystem.LoadLevel();
        SaveLevelSystem.levelToSave.IsBonused = true;
        SaveLevelSystem.SaveLevel();
        SceneManager.LoadScene(prevScene);
    }
}
