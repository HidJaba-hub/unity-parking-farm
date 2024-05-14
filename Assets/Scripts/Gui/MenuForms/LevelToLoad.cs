using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad
{
    public static int GetLastLevel()
    {
        SaveLevelSystem.LoadLevel();
        if (SaveLevelSystem.levelToSave == null)
        {
            SceneManager.LoadScene("Level1_Designed");
            return 0;
        }
        
        return SaveLevelSystem.levelToSave.IsCompleted ? SaveLevelSystem.levelToSave.LevelInd: SaveLevelSystem.levelToSave.LevelInd-1;
    }
}
