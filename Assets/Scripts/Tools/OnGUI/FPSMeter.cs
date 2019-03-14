using UnityEngine;

[ExecuteInEditMode]
public class FPSMeter : MonoBehaviour
{
#pragma warning disable IDE0044 // Add readonly modifier
    [SerializeField]
    private Color TextColor = Color.black;
    [SerializeField]
    private int FontSize = 50;
    [SerializeField]
    private int PaddingTop = 20;
    [SerializeField]
    private int PaddingLeft = 20;
#pragma warning restore IDE0044 // Add readonly modifier

    private readonly GUIStyle debugGUIStyle = new GUIStyle();

    private void Awake()
    {
        ApplyInspectorSettings();
    }

    private void ApplyInspectorSettings()
    {
        debugGUIStyle.normal.textColor = TextColor;
        debugGUIStyle.fontSize = FontSize;
        debugGUIStyle.padding = new RectOffset(PaddingLeft, 0, PaddingTop, 0);
    }

    private void OnGUI()
    {

        GUILayout.Label((1.0 / Time.deltaTime).ToString("0.0"), debugGUIStyle);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyInspectorSettings();
    }
#endif
}