using UnityEngine;

public class PadLateralMove : MonoBehaviour
{
    public float CurrentPosition
    {
        get
        {
            return transform.position.x;
        }
        set
        {
            if (value > transform.position.x) MovementDirection = 1;
            else if (value < transform.position.x) MovementDirection = -1;
            else
            {
                MovementDirection = 0;
                return;
            }
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }
    }
    public float Speed { get; set; }
    public float Acceleration { get { return acceleration; } }
    public float MaxSpeed { get { return maxSpeed; } }
    public int AccelerationDirection
    {
        get
        {
            if (controller.Position >= transform.position.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
    public int MovementDirection { get; private set; }
    // PAD API
    public void Init()
    {
#if UNITY_EDITOR
        controller = new MousePadLateralInput();
#else
        controller = new TouchPadLateralInput();
#endif
        state = new IdleState();
        state.Enter(this);
    }

    public void Tick(float deltaTime)
    {
        var newState = state.HandleInput();
        if (newState != null)
        {
            state.Exit();
            state = newState;
            state.Enter(this);
        }
        state.Update(deltaTime);
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
    }
    public void SetAcceleration(float acceleration)
    {
        this.acceleration = acceleration;
    }
    // INPUT API
    public bool MovementIsRequested { get { return controller.InputPressed; } }

    public bool TargetPositionIsReached()
    {
        return DistanceToTarget() <= movementThreshold;
    }

    public bool MaxSpeedIsReached()
    {
        return Speed == MaxSpeed;
    }

    // IMPLEM
    private State state;
    private IPadInput controller;

    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float movementThreshold;
    [SerializeField]
    private float maxSpeed;

    private float DistanceToTarget() {
        return Mathf.Abs(controller.Position - transform.position.x);
    }
}
