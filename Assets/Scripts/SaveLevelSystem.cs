using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLevelSystem : MonoBehaviour
{
    public static LevelToSave levelToSave;

    public static void InitializeCurrentLevel(int index)
    {
        LoadLevel();

        if(levelToSave == null || levelToSave.LevelInd != index) 
            levelToSave = new LevelToSave(index);
    }
    public static void SaveLevel()
    {
        PlayerPrefs.SetString("LastLevelLoaded", JsonConvert.SerializeObject(levelToSave));
        PlayerPrefs.Save();
    }

    public static void LoadLevel()
    {
        levelToSave = JsonConvert.DeserializeObject<LevelToSave>(PlayerPrefs.GetString("LastLevelLoaded"));
    }
}
