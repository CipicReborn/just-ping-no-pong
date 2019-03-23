using UnityEngine;

public class PadModel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer contactZone;

    private Color initialContactZoneColor;

    private void Awake()
    {
        initialContactZoneColor = contactZone.material.color;
    }

    public void SetContactZoneColor(Color color)
    {
        contactZone.material.color = color;
    }

    public void ResetContactZoneColor()
    {
        contactZone.material.color = initialContactZoneColor;
    }

}
