using UnityEngine;

[ExecuteInEditMode]
public class DebugOnGUI : MonoBehaviour
{
#pragma warning disable IDE0044 // Add readonly modifier
    [Header("GUI Settings")]
    [SerializeField]
    private Color TextColor = Color.black;
    [SerializeField]
    private int FontSize = 50;
    [SerializeField]
    private int PaddingTop = 20;
    [SerializeField]
    private int PaddingLeft = 20;
    [SerializeField]
    private int Spacing = 0;

    [Header("GameObject")]
    [SerializeField]
    private Pad Target;
#pragma warning restore IDE0044 // Add readonly modifier


    private readonly GUIStyle debugGUIStyle = new GUIStyle();
    private readonly GUIStyle debugGUIStyle2 = new GUIStyle();


    private void Awake()
    {
        ApplyInspectorSettings();
    }

    private void ApplyInspectorSettings()
    {
        debugGUIStyle.normal.textColor = TextColor;
        debugGUIStyle.fontSize = FontSize;
        debugGUIStyle.padding = new RectOffset(PaddingLeft, 0, PaddingTop, 0);

        debugGUIStyle2.normal.textColor = TextColor;
        debugGUIStyle2.fontSize = FontSize;
        debugGUIStyle2.padding = new RectOffset(PaddingLeft, 0, Spacing, 0);
    }

    private void OnGUI()
    {
        if (Target.Input != null)
        {
            GUILayout.Label(Target.Input.RawVerticalDelta.ToString("0.0"), debugGUIStyle);
            GUILayout.Label(Target.Input.Rotation.ToString("0.0"), debugGUIStyle2);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyInspectorSettings();
    }
#endif
}