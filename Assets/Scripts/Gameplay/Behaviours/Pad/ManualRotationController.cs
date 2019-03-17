using UnityEngine;

public class ManualRotationController : RotationController
{
#pragma warning disable CS0649 // field nerver assigned
    [SerializeField]
    private ManualRotationControllerData data;
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
            return Mathf.Min(Mathf.Max(input.Rotation * data.ZRotationSpeed, -data.MaximumZRotation), data.MaximumZRotation);

        }
        else
        {
            return transform.rotation.eulerAngles.z;
        }
    }

}
