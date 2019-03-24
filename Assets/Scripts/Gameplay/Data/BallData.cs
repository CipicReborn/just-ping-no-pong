using UnityEngine;

[CreateAssetMenu(fileName ="BallData", menuName = "Just Ping No Pong/Gameplay/New Ball Data")]
public class BallData : ScriptableObject
{
    public float Mass = 0.1f;
    public float Drag = 0.2f;
    public float Diameter = 1.0f;
}
