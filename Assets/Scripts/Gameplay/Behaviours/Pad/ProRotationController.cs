using UnityEngine;

public class ProRotationController : RotationController
{

#pragma warning disable CS0649 // field nerver assigned
    [SerializeField]
    private ManualRotationControllerData data;
    [SerializeField]
    private GameManager gm;
#pragma warning restore CS0649 // field nerver assigned

    private IPadInput input;
    private bool spinning;
    private float timeTargetForSpinScoring;
    private float rotation;

    public override void Init(Pad pad)
    {
        input = pad.Input;
    }

    public override float GetZAngle()
    {
        Tick(Time.deltaTime);
        return rotation;
        //if (spinning) return GetZAngleSpinControl();
        //else return GetZAngleNormalControl();
    }

    
    private void Tick(float deltaTime)
    {
        wasPressedLastTick = isPressed;
        isPressed = input.InputPressed;

        if (!spinning && input.InputPressed && !wasPressedLastTick && ((Time.time - lastPressTime) < Limit))
        {
            Debug.Log(Time.time + " - " + lastPressTime + " = " + (Time.time - lastPressTime) + " < " + Limit);
            spinning = true;
            timeTargetForSpinScoring = Time.time;
            direction = Random.Range(0, 1f) > 0.5f ? 1 : -1;
        }
        if (spinning && !input.InputPressed)
        {
            spinning = false;
        }

        if (isPressed)
        {
            lastPressTime = Time.time;
            //Debug.Log("Last Press Time : " + lastPressTime);
        }
        float rotation;

        if (spinning)
        {
            rotation = transform.rotation.eulerAngles.z + direction * spinningSpeed * deltaTime;
            if (Time.time > timeTargetForSpinScoring)
            {
                timeTargetForSpinScoring = Time.time + ScoringPeriod;
                gm.AddScoreForSpinning();
            }
            //Debug.Log("Spinning = " + rotation);
        }
        else
        {
            if (isPressed)
            {
                var inputClamped = Mathf.Clamp(input.Rotation, -ControlDepth, ControlDepth);
                rotation = ((inputClamped + ControlDepth) * MaxAngle / ControlDepth) - MaxAngle;
                //Debug.Log("Rotating = " + rotation);
            }
            else
            {
                rotation = transform.rotation.eulerAngles.z;
                //Debug.Log("Stopped = " + rotation);
            }
        }
        this.rotation = rotation;
    }

    [Range(10, 100)]
    public float ControlDepth = 80;
    public float MaxAngle = 180;
    public float Limit = 1;
    public float spinningSpeed;
    public float ScoringPeriod;

    private bool isPressed;
    private bool wasPressedLastTick;
    private float lastPressTime;
    private int direction;

    private float GetZAngleNormalControl()
    {
        if (input.InputPressed)
        {
            var inputClamped = Mathf.Clamp(input.Rotation, -ControlDepth, ControlDepth);
            return ((inputClamped + ControlDepth) * MaxAngle / ControlDepth) - MaxAngle;
        }
        else
        {
            return transform.rotation.eulerAngles.z;
        }
    }

    private float GetZAngleSpinControl()
    {
        if (input.InputPressed)
        {
            return Mathf.Min(Mathf.Max(input.Rotation * data.ZRotationSpeed, -data.MaximumZRotation), data.MaximumZRotation);

        }
        else
        {
            return transform.rotation.eulerAngles.z;
        }
    }
}
