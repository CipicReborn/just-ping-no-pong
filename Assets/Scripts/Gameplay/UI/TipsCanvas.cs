using UnityEngine;
using UnityEngine.UI;

public class TipsCanvas : MonoBehaviour
{
    [SerializeField]
    private Button NextButton;

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
