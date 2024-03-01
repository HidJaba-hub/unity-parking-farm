using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class AnimationToggler
{
    private readonly Animator _farmerAnimator;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Side = Animator.StringToHash("Side");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Dodge = Animator.StringToHash("Dodge");

    public AnimationToggler(Animator animator)
    {
        _farmerAnimator = animator;
    }
    
    public void FarmerRun(bool state)
    {
        _farmerAnimator.SetBool(Running, state);
    }

    public void FarmerTurn(int sideIndex)
    {
        _farmerAnimator.SetInteger(Side, sideIndex);
    }

    public void FarmerJump()
    {
        _farmerAnimator.SetBool(Jump, true);
    }

    public bool FarmerDodge()
    {
        if (_farmerAnimator.GetBool(Dodge)) return false;
        _farmerAnimator.SetBool(Dodge, true);
        return true;
    }
}
