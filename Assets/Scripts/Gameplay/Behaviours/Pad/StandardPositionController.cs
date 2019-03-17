using UnityEngine;

public class StandardPositionController : PositionController
{
#pragma warning disable CS0649 // field never assigned to;
    [SerializeField]
    private StandardPositionControllerData data;
#pragma warning restore CS0649

    public override void Init(Pad pad)
    {
        this.pad = pad;
        input = pad.Input;

        rotationModeSwitchPosition = pad.ScreenRightLimit - 2 * data.PadWidth;
        float maximumYRotation = Mathf.Asin(data.PadWidth / data.PadLength) * Mathf.Rad2Deg;
        rotationFactor = maximumYRotation / (ScreenRightLimit - 2 * data.PadWidth);
    }

    public override float GetPosition()
    {
        return input.Position;
    }

    public override float GetYAngle()
    {
        if (input.InputPressed)
        {
            if (input.Position < -rotationModeSwitchPosition)
            {
                var x = input.Position + ScreenRightLimit;
                return Mathf.Asin((data.PadWidth - x) / data.PadLength) * Mathf.Rad2Deg;
            }
            else if (input.Position > rotationModeSwitchPosition)
            {
                var x = ScreenRightLimit - input.Position;
                return Mathf.Asin((x - data.PadWidth) / data.PadLength) * Mathf.Rad2Deg;
            }
            else
            {
                return input.Position * rotationFactor;
            }
        }
        else return transform.rotation.eulerAngles.y;
    }


    //calc
    private float rotationModeSwitchPosition;
    private float rotationFactor;

    private Pad pad;
    private IPadInput input;
    private float ScreenRightLimit { get { return pad.ScreenRightLimit; } }
}
