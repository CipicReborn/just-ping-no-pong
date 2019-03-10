using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Just Ping No Pong/Game Design/New Equipment")]
public class Equipment : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public GameObject GamePrefab;
}
