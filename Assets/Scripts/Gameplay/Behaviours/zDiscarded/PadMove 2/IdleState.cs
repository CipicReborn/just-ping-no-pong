using UnityEngine;

public class IdleState : State
{
    // STATE API
    public override void Enter(PadLateralMove pad)
    {
        this.pad = pad;
        pad.Speed = 0;
        Debug.Log("Entering Idle");
    }
    public override void Exit() { Debug.Log("Leaving Idle"); }
    public override State HandleInput()
    {   
        if (pad.MovementIsRequested && !pad.TargetPositionIsReached())
        {
            Debug.Log("Input detected to new target position");
            return new AccelerationState();
        }
        return null;
    }

    public override void Update(float deltaTime) { }

    // IMPLEM
    private PadLateralMove pad;
}
