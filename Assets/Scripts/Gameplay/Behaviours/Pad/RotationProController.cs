using UnityEngine;

public class RotationProController : RotationController
{
#pragma warning disable CS0649 // field nerver assigned
    [SerializeField]
    private RotationProControllerData data;
#pragma warning restore CS0649 // field nerver assigned

    IPadInput input;

    public override void Init(Pad pad)
    {
        input = pad.Input;
    }

    public override float GetZAngle()
    {
        if (input.InputPressed)
        {
            var inputClamped = Mathf.Clamp(input.Rotation, -data.ControlDepth, data.ControlDepth);
            return ((inputClamped + data.ControlDepth) * data.MaxAngle / data.ControlDepth) - data.MaxAngle;
        }
        else
        {
            return transform.rotation.eulerAngles.z;
        }
    }

}
