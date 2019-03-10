using UnityEngine;
using UnityEngine.UI;

public class TipsCanvas : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044

    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private float MinimumDisplayTime;

#pragma warning restore IDE0044
#pragma warning restore CS0649
    #endregion

    public void OnEnable()
    {
        NextButton.interactable = false;
        Invoke("EnableNext", MinimumDisplayTime);
    }

    public void EnableNext()
    {
        NextButton.interactable = true;
    }
    
}
