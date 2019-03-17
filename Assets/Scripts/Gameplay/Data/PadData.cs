using UnityEngine;

[CreateAssetMenu(fileName = "Pad", menuName ="Just Ping No Pong/Gameplay/New Pad Data")]
public class PadData : ScriptableObject
{
    [Header("Rebound")]
    public float ReboundForce = 20f;
}
