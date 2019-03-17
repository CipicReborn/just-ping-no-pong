using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PadInfo", menuName = "Just Ping No Pong/Game Design/New Pad Info")]
public class PadInfo : ScriptableObject
{
    [Serializable]
    public class AttributeData
    {
        public string Name;
        public int Value;

        public AttributeData(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    public string PadName;
    public string Description;
    public List<AttributeData> Attributes = new List<AttributeData>
    {
        { new AttributeData("Move", 3) },
        { new AttributeData("Spin", 1) },
        { new AttributeData("Special", 0) },
    };
}
