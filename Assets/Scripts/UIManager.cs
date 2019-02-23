
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ScoreTextGUI;
    [SerializeField]
    private HandleUI handleGUI;

    public void UpdateScoreGUI(int score)
    {
        ScoreTextGUI.text = score.ToString();
    }

    public void UpdatePadGUI(float normalisedPadPosition)
    {
        handleGUI.UpdatePadUIFeedback(normalisedPadPosition);
    }
}
