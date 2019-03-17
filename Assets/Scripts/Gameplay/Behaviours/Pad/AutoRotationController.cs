using UnityEngine;

public class AutoRotationController : RotationController
{
    public RotationAutoControllerData data;
    [Range(0, 1f)]
    public float LerpK;

    public float threshold = 0.5f;
    private int side = 1;
    private Quaternion targetRotation = Quaternion.identity;

    public override void Init(Pad pad)
    {
        Debug.Log("Automatic Rotation Controller Enabled");
    }

    public override float GetZAngle()
    {
        var distance = targetRotation * Quaternion.Inverse(transform.rotation);
        if (distance.eulerAngles.z < threshold)
        {
            DetermineTargetRotation();
        }

        return Quaternion.Lerp(transform.rotation, targetRotation, LerpK).eulerAngles.z;
    }

    public Quaternion GetZRotation()
    {
        Quaternion target = Quaternion.identity;
        var distance = target * Quaternion.Inverse(transform.rotation);
        if (distance.eulerAngles.z < threshold)
        {
            DetermineTargetRotation();
        }

        return Quaternion.Lerp(transform.rotation, target, LerpK);
    }

    private void DetermineTargetRotation()
    {
        targetRotation = Quaternion.Euler(0, 0, Random.Range(0, data.MaxAngle * side));
        side *= -1;
    }
}
