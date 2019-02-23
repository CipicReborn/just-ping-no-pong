using UnityEngine;

public class SlowDownState : State
{
    // STATE API
    public override void Enter(PadLateralMove pad)
    {
        this.pad = pad;
        direction = pad.AccelerationDirection;
        Debug.Log("Entering SlowDown");
    }
    public override void Exit()
    {
        Debug.Log("Leaving Slowdown");
    }
    public override State HandleInput()
    {
        if (pad.TargetPositionIsReached())
        {
            Debug.Log("Target Position Is Reached");
            return new IdleState();
        }
        else if (pad.MovementIsRequested)
        {
            Debug.Log("Input detected");
            return new AccelerationState();
        }
        else if (pad.Speed == 0)
        {
            Debug.Log("Reached 0 Speed");
            return new IdleState();
        }
        return null;
    }

    public override void Update(float deltaTime)
    {
        pad.Speed = Mathf.Max(pad.Speed - pad.Acceleration * deltaTime, 0);
        pad.CurrentPosition += pad.Speed * direction * deltaTime;
    }

    // IMPLEM
    private PadLateralMove pad;
    private float direction;
}