using UnityEngine;

public class HandleUI : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
    #pragma warning disable CS0649

    [SerializeField]
    private RectTransform HandleTransform;
    
    #pragma warning restore CS0649
    #endregion

    private new RectTransform transform;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();

        //Debug.Log("Handle Transform : " + transform);
    }

    public void UpdatePadUIFeedback(float normalisedPosition)
    {
        //Debug.Log("Normalised position : " + normalisedPosition.ToString());
        var clampedPosition = Mathf.Max(Mathf.Min(normalisedPosition, transform.anchorMax.x), transform.anchorMin.x);
        //Debug.Log("Clamped position : " + clampedPosition.ToString());
        var rescaledPosition = clampedPosition * (transform.anchorMax.x - transform.anchorMin.x) + transform.anchorMin.x;
        //Debug.Log("Rescaled position : " + rescaledPosition.ToString());
        HandleTransform.anchorMin = new Vector2(rescaledPosition, 0);
        HandleTransform.anchorMax = new Vector2(rescaledPosition, 1);
    }
}