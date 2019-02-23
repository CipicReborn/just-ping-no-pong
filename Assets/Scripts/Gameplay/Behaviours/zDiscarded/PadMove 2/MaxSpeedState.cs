using UnityEngine;

public class MaxSpeedState : State
{
    // STATE API
    public override void Enter(PadLateralMove pad)
    {
        this.pad = pad;
        Debug.Log("Entering MaxSpeed");
    }
    public override void Exit()
    {
        Debug.Log("Leaving MaxSpeed");
    }
    public override State HandleInput()
    {
        if (pad.TargetPositionIsReached())
        {
            Debug.Log("Target Position Is Reached");
            return new IdleState();
        }
        if(pad.MovementIsRequested)
        {
            if (pad.AccelerationDirection != pad.MovementDirection)
            {
                return new AccelerationState();
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.Log("Input stopped");
            return new SlowDownState();
        }
    }

    public override void Update(float deltaTime)
    {
        pad.CurrentPosition += pad.Speed * pad.AccelerationDirection * deltaTime;
    }

    // IMPLEM
    private PadLateralMove pad;
}
