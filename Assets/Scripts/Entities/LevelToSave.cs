using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToSave 
{
    public int LevelInd { get; set; }
    public bool IsBonused { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public bool hasTimer { get; set; }
    public bool hasCountDown { get; set; }
    //public List<LooseCondition> _looseConditions  = new List<LooseCondition>();

    public LevelToSave(int index)
    {
        LevelInd = index;
        IsBonused = false;
        IsCompleted = false;
    }

    public void AddLooseCondition(LooseCondition looseCondition)
    {
        if (looseCondition is Timer) hasTimer = true;
        if (looseCondition is CountDown) hasCountDown = true;
    }
    
}
