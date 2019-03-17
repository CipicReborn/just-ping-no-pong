using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public List<Color> Colors;
    public Color Empty;

    public void SetValue(int value)
    {
        value = Mathf.Clamp(value, 0, transform.childCount);

        for (int i = 0; i < value; i++)
        {
            var image = transform.GetChild(i).GetComponent<Image>();
            image.color = Colors[i];
        }

        for (int i = value; i < transform.childCount; i++)
        {
            var image = transform.GetChild(i).GetComponent<Image>();
            image.color = Empty;
        }
    }
}
