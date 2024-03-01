using System.Collections;
using System.Collections.Generic;
using Runner.Moving;
using UnityEngine;

public class PlayerMove : Move
{
    public Animator farmerAnimator;
    public float jumpHeight = 1.0f;
    
    private float _gravityValue = -9.81f;

    private AnimationToggler _animationToggler;

    private new void Start()
    {
        base.Start();
        _animationToggler = new AnimationToggler(farmerAnimator);
    }
    protected new void Update()
    {
        base.Update();
    }
    protected override void ChangeSide()
    {
        int sign = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sign = 1;
            _animationToggler.FarmerTurn(sign);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            sign = -1;
            _animationToggler.FarmerTurn(sign);
        }
        else
        {
            return;
        }
        
        LaneNumber += sign;
        LaneNumber = Mathf.Clamp(LaneNumber, 0, LaneCount);
    }
    protected override void Jump()
    {
        if (Controller.isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            _animationToggler.FarmerJump();
            PlayerVelocity.y = 0;
            while (PlayerVelocity.y < 3f)
            {
                PlayerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * _gravityValue);
            }
        }
        else
            PlayerVelocity.y += _gravityValue * Time.smoothDeltaTime;
    }

    public void SpeedIncrease()
    {
        moveSpeed++;
    }

    public void SpeedDecrease()
    {
        if(_animationToggler.FarmerDodge()) moveSpeed--;
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
