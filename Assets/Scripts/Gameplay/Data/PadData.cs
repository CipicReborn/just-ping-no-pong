using UnityEngine;

[CreateAssetMenu(fileName = "Pad", menuName ="Just Ping No Pong/Gameplay/New Pad Data")]
public class PadData : ScriptableObject
{
    [Header("Handling")]
    public float LerpFactor = 0.4f;
    public float MaximumZRotation = 25f;
    public float ZRotationSpeed = 2f;
    public float ReboundForce = 20f;
    [Header("Geometry")]
    public float PadLength = 0.75f;
    public float PadWidth = 0.5f;
}
