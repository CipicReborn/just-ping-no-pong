
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ScoreTextGUI;
    [SerializeField]
    private HandleUI handleGUI;
    [SerializeField]
    private Animator scoreAnimator;
    [SerializeField]
    private new Camera camera;

    private TextMeshProUGUI scoreText;
    private readonly int scoreAnimationHash = Animator.StringToHash("ScoreAnimation");

    private void Awake()
    {
        scoreText = scoreAnimator.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreGUI(int scored, int totalScore, Vector3 worldPosition)
    {
        ScoreTextGUI.text = totalScore.ToString();
        if (scored > 0)
        {
            scoreAnimator.transform.position = camera.WorldToScreenPoint(worldPosition);
            scoreText.text = string.Format("+{0}", scored);
            scoreAnimator.Play(scoreAnimationHash, 0, 0);
        }
    }

    public void UpdatePadGUI(float normalisedPadPosition)
    {
        handleGUI.UpdatePadUIFeedback(normalisedPadPosition);
    }
}
