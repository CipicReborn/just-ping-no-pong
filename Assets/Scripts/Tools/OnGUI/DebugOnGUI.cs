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
    [SerializeField]
    private Ball Ball;
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
    float maxPos = 0;
    float maxVelo = 0;
    private void OnGUI()
    {
        maxPos = Mathf.Max(maxPos, Ball.transform.position.y);
        maxVelo = Mathf.Max(maxVelo, Ball.Velocity.y);
        GUILayout.Label("", debugGUIStyle);
        GUILayout.Label("Ball Mass : " + Ball.Mass.ToString("0.0"), debugGUIStyle);
        GUILayout.Label("Force Applied : " + Target.Force.y.ToString("0.0"), debugGUIStyle);
        GUILayout.Label("Ball Y Position : " + Ball.transform.position.y.ToString("0.0"), debugGUIStyle);
        GUILayout.Label("Ball Y Velocity : " + Ball.Velocity.y.ToString("0.0"), debugGUIStyle);
        GUILayout.Label("Ball Max Y Pos : " + maxPos.ToString("0.00"), debugGUIStyle);
        GUILayout.Label("Ball Max Y Velo : " + maxVelo.ToString("0.00"), debugGUIStyle);
        
        //if (Target.Input != null)
        //{
        //    GUILayout.Label(Target.Input.RawVerticalDelta.ToString("0.0"), debugGUIStyle);
        //    GUILayout.Label(Target.Input.Rotation.ToString("0.0"), debugGUIStyle2);
        //}
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyInspectorSettings();
    }
#endif
}