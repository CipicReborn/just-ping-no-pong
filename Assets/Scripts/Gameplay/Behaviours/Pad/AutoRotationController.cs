using UnityEngine;

public class AutoRotationController : RotationController
{

    public ManualRotationControllerData data;
    public bool logEnabled;
    [Range(0, 1f)]
    public float lerp;

    public override void Init(Pad pad)
    {
        Debug.Log("Automatic Rotation Controller Enabled");
        //Log();
    }

    //private void Log()
    //{
    //    Debug.Log("current : " + transform.rotation.eulerAngles.z + ", target : " + targetRotation.eulerAngles.z);
    //    Debug.Log("distance : " + (targetRotation * Quaternion.Inverse(transform.rotation)).eulerAngles.z);
    //    Debug.Log("next Move : " + Quaternion.Lerp(transform.rotation, targetRotation, 0.6f).eulerAngles.z);
    //}

    public override float GetZAngle()
    {
        //if (logEnabled) Log();

        //if (Mathf.Abs(transform.rotation.eulerAngles.z - targetRotation) < threshold)
        //{
        //    DetermineTargetRotation();
        //}

        //return Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, 0.6f);

        var distance = targetRotation * Quaternion.Inverse(transform.rotation);
        if (distance.eulerAngles.z < threshold)
        {
            DetermineTargetRotation();
        }

        return Quaternion.Lerp(transform.rotation, targetRotation, lerp).eulerAngles.z;
    }

    public Quaternion GetZRotation()
    {
        //if (logEnabled) Log();
        Quaternion target = Quaternion.identity;
        var distance = target * Quaternion.Inverse(transform.rotation);
        if (distance.eulerAngles.z < threshold)
        {
            DetermineTargetRotation();
        }

        return Quaternion.Lerp(transform.rotation, target, lerp);

    }

    private int side = 1;
    private Quaternion targetRotation = Quaternion.identity;
    public float threshold = 0.5f;

    private void DetermineTargetRotation()
    {
        targetRotation = Quaternion.Euler(0, 0, Random.Range(0, data.MaximumZRotation * side));
        Debug.Log("New Target Rotation : " + targetRotation.eulerAngles);
        side *= -1;
    }

}
