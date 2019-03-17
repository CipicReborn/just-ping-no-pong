using UnityEngine;

public class RotationMasterController : RotationController
{

#pragma warning disable CS0649 // field nerver assigned
    [SerializeField]
    private RotationMasterControllerData data;
    [SerializeField]
    private GameManager gm;
#pragma warning restore CS0649 // field nerver assigned

    private IPadInput input;
    private bool spinning;
    private float SpinTimeToBeat;
    private float rotation;
    private bool isPressed;
    private bool wasPressed;
    private float lastPressTime;
    private int direction;

    public override void Init(Pad pad)
    {
        input = pad.Input;
    }

    public override float GetZAngle()
    {
        Tick(Time.deltaTime);
        return rotation;
    }
    
    private void Tick(float deltaTime)
    {
        wasPressed = isPressed;
        isPressed = input.InputPressed;

        if (!spinning && input.InputPressed && !wasPressed && ((Time.time - lastPressTime) < data.Limit))
        {
            Debug.Log(Time.time + " - " + lastPressTime + " = " + (Time.time - lastPressTime) + " < " + data.Limit);
            spinning = true;
            SpinTimeToBeat = Time.time;
            direction = Random.Range(0, 1f) > 0.5f ? 1 : -1;
        }
        if (spinning && !input.InputPressed)
        {
            spinning = false;
        }

        if (isPressed)
        {
            lastPressTime = Time.time;
        }

        if (spinning)
        {
            rotation = transform.rotation.eulerAngles.z + direction * data.SpinningSpeed * deltaTime;

            if (Time.time > SpinTimeToBeat)
            {
                SpinTimeToBeat = Time.time + data.ScoringPeriod;
                gm.AddScoreForSpinning();
            }
        }
        else
        {
            if (isPressed)
            {
                var inputClamped = Mathf.Clamp(input.Rotation, -data.ControlDepth, data.ControlDepth);
                rotation = ((inputClamped + data.ControlDepth) * data.MaxAngle / data.ControlDepth) - data.MaxAngle;
            }
            else
            {
                rotation = transform.rotation.eulerAngles.z;
            }
        }
    }
}
