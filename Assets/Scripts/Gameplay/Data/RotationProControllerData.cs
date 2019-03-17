using UnityEngine;

[CreateAssetMenu(fileName = "RotationProControllerData", menuName = "Just Ping No Pong/Gameplay/New RotationProController Data")]
public class RotationProControllerData : ScriptableObject
{
    public float MaxAngle = 20;
    [Range(10, 100)]
    public float ControlDepth = 70;
}
