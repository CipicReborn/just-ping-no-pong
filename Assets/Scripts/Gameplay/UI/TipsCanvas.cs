using UnityEngine;
using UnityEngine.UI;

public class TipsCanvas : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
    #pragma warning disable CS0649 // Disable warning "variable never initialized"

    [SerializeField]
    private Button NextButton;

    #pragma warning restore CS0649
    #endregion

    public void OnEnable()
    {
        NextButton.interactable = false;
        Invoke("EnableNext", 3f);
    }

    public void EnableNext()
    {
        NextButton.interactable = true;
    }
    
}
