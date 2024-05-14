using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToChoose : MonoBehaviour
{
    public string levelSceneToLoad;

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelSceneToLoad);
    }
}
