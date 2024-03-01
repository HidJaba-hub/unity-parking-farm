using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateBehaviour : StateMachineBehaviour
{
    private static readonly int Side = Animator.StringToHash("Side");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Dodge = Animator.StringToHash("Dodge");
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("TurnLeft") || stateInfo.IsName("TurnRight"))
        {
            animator.SetInteger(Side, 0);
        }
        if (stateInfo.IsName("Jump"))
        {
            animator.SetBool(Jump, false);
        }
        if (stateInfo.IsName("Dodge"))
        {
            animator.SetBool(Dodge, false);
        }
    }
}
