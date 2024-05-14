using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class LevelForm : MonoBehaviour
{
    public List<LevelToChoose> levels;
    public Button buttonIncrement;
    public Button buttonDecrement;
    public Button playButton;
    public Button lockButton;
    private int _indexToShow;
    private int _maxLoadedIndex;
    private void Awake()
    {
        _indexToShow = 0;
        levels[_indexToShow].gameObject.SetActive(true);
        buttonDecrement.onClick.AddListener(DecrementLevel);
        buttonIncrement.onClick.AddListener(IncrementLevel);

        _maxLoadedIndex = LevelToLoad.GetLastLevel();
        ChangeButtonsState();
    }
    public void IncrementLevel()
    {
        if (_indexToShow == levels.Count - 1) return;
        _indexToShow++;
        ActivateLevelForm(levels[_indexToShow].gameObject, true);
        ActivateLevelForm(levels[_indexToShow-1].gameObject, false);
        ChangeButtonsState();
    }
    public void DecrementLevel()
    {
        _indexToShow--;
        ActivateLevelForm(levels[_indexToShow].gameObject, true);
        ActivateLevelForm(levels[_indexToShow+1].gameObject, false);
        ChangeButtonsState();
    }

    private void ChangeButtonsState()
    {
        if(_indexToShow == 0) 
            buttonDecrement.gameObject.SetActive(false);
        if(_indexToShow != 0 && !buttonDecrement.gameObject.activeSelf) 
            buttonDecrement.gameObject.SetActive(true);
        if(_indexToShow == levels.Count - 1) 
            buttonIncrement.gameObject.SetActive(false);
        if(_indexToShow != levels.Count - 1 && !buttonIncrement.gameObject.activeSelf) 
            buttonIncrement.gameObject.SetActive(true);
    }
    public void ActivateLevel()
    {
        levels[_indexToShow].LoadLevelScene();
    }
    private void ActivateLevelForm(GameObject level, bool isActivated)
    {
        if (_indexToShow > _maxLoadedIndex)
        {
            lockButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }
        else
        {
            lockButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
        level.SetActive(isActivated);
    }
}
