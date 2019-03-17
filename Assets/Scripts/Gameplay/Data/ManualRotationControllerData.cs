using UnityEngine;

[CreateAssetMenu(fileName = "ManualRotationControllerData", menuName = "Just Ping No Pong/Gameplay/New ManualRotationController Data")]
public class ManualRotationControllerData : ScriptableObject
{
    public float MaximumZRotation = 25f;
    public float ZRotationSpeed = 2f;
}
