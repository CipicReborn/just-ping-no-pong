using UnityEngine;

public abstract class RotationController : MonoBehaviour {
    public abstract void Init(Pad pad);
    public abstract float GetZAngle();
}
