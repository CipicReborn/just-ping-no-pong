using UnityEngine;

public abstract class PositionController : MonoBehaviour {
    public abstract void Init(Pad pad);
    public abstract float GetPosition();
    public abstract float GetYAngle();
}
