using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : Move
{
    public Animator henAnimator;
    public int side = 0;
    public Dictionary<float, int>.Enumerator instructions;

    private float _chooseSidePoint = 0;
    private float gravityValue = -9.81f;
    private AnimationToggler _animationToggler;

    private new void Start()
    {
        base.Start();
        _animationToggler = new AnimationToggler(henAnimator);
    }
    protected override void ChangeSide()
    {
        if (_chooseSidePoint != 0 && transform.position.z >= _chooseSidePoint)
        {
            side = instructions.Current.Value;
            LaneNumber = side + 1;
            _chooseSidePoint = 0;
        }
        if (_chooseSidePoint == 0 && instructions.MoveNext())
        {
            _chooseSidePoint = instructions.Current.Key - 2f;
        }
        LaneNumber = Mathf.Clamp(LaneNumber, 0, LaneCount);
    }
    protected override void Jump()
    {
        PlayerVelocity.y += gravityValue * Time.smoothDeltaTime;
    }
    public override void StopRun()
    {
        base.StopRun();
        _animationToggler.FarmerRun(false);
    }
    public override void Run()
    {
        base.Run();
        _animationToggler.FarmerRun(true);
    }
}
