using UnityEngine;

public class AccelerationState : State
{
    // STATE API
    public override void Enter(PadLateralMove pad)
    {
        this.pad = pad;
        Debug.Log("Entering Acceleration");
    }
    public override void Exit()
    {
        Debug.Log("Leaving Acceleration");
    }
    public override State HandleInput()
    {
        if (pad.MovementIsRequested)
        {
            if (pad.TargetPositionIsReached())
            {
                Debug.Log("Target Position Reached");
                return new IdleState();
            }
            if (pad.MaxSpeedIsReached()){
                Debug.Log("Max Speed Reached");
                return new MaxSpeedState();
            }
            return null;
        }
        else
        {
            Debug.Log("Input Stopped");
            return new SlowDownState();
        }
    }

    public override void Update(float deltaTime)
    {
        if (pad.MovementDirection == 0 || pad.AccelerationDirection == pad.MovementDirection)
            pad.Speed = Mathf.Min(pad.Speed + pad.Acceleration * deltaTime, pad.MaxSpeed);
        else
        {
            pad.Speed = Mathf.Max(pad.Speed - pad.Acceleration * deltaTime, 0);
        }
        pad.CurrentPosition += pad.Speed * pad.AccelerationDirection * deltaTime;
    }
    
    // IMPLEM
    private PadLateralMove pad;
}
