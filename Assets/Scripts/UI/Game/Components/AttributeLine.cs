using TMPro;
using UnityEngine;

public class AttributeLine : MonoBehaviour
{
    public TextMeshProUGUI Label;
    public Gauge Gauge;

    public void Setup(PadInfo.AttributeData attribute)
    {
        Setup(attribute.Name, attribute.Value);
    }

    public void Setup(string name, int value)
    {
        Label.text = name;
        Gauge.SetValue(value);
    }
}
