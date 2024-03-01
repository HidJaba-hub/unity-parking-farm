using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isStop = false;
    public float moveSpeed = 10.0f;
    public float firstLanePos, laneDistance, sideSpeed;

    protected Vector3 PlayerVelocity;
    public Vector3 move;
    protected int LaneNumber = 1, LaneCount = 2;

    protected CharacterController Controller;

    protected void Start()
    {
        isStop = true;
        TryGetComponent<CharacterController>(out Controller);
        move = new Vector3(0, 0, 1);
    }

    protected void Update()
    {
        if (isStop) return;
        
        Jump();

        move.z = moveSpeed;
        move *= Time.smoothDeltaTime;
        move += PlayerVelocity * Time.smoothDeltaTime;

        ChangeSide();

        Controller.Move(move);

        SideMove();
    }

    private void SideMove()
    {
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, firstLanePos + (LaneNumber * laneDistance), Time.smoothDeltaTime * sideSpeed);
        transform.position = newPos;
    }

    protected virtual void ChangeSide()
    {
    }

    protected virtual void Jump()
    {
    }

    public virtual void Run()
    {
        isStop = false;
    }
    public virtual void StopRun()
    {
        isStop = true;
    }
}
