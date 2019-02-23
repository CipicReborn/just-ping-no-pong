﻿using UnityEngine;

[CreateAssetMenu(fileName ="BallData", menuName ="Gameplay/New Ball Data")]
public class BallData : ScriptableObject
{
    public float Mass = 0.1f;
    public float Radius = 0.3f;
}