using UnityEngine;

[CreateAssetMenu(fileName = "RotationMasterControllerData", menuName = "Just Ping No Pong/Gameplay/New RotationMasterController Data")]
public class RotationMasterControllerData : ScriptableObject
{
    public float MaxAngle = 45;
    [Range(10, 100)]
    public float ControlDepth = 80;
    public float Limit = 0.12f;
    public float SpinningSpeed = 3240;
    public float ScoringPeriod = 0.5f;
}
