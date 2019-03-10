using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameDesignData", menuName="Just Ping No Pong/Game Design/New GameDesignData")]
public class GameDesignData : ScriptableObject
{


    public List<Mission> Missions = new List<Mission>();
}
