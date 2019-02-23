using UnityEngine;

public class HandleUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform handleTransform;
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
        handleTransform.anchorMin = new Vector2(rescaledPosition, 0);
        handleTransform.anchorMax = new Vector2(rescaledPosition, 1);
    }
}