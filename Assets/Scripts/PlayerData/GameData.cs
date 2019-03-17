using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Just Ping No Pong/Player Data/New Game Data")]
public class GameData : ScriptableObject
{
    [Serializable]
    public class HiScore
    {
        string name;
        int score;
    }



    public List<HiScore> HiScores = new List<HiScore>();
    public int Level = 1;

    //public PadAsset EquippedPad;
    //public BallAsset EquippedBall;



}
