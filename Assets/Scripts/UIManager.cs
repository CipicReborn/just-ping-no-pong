
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
    #pragma warning disable CS0649

    [SerializeField]
    private Text ScoreTextGUI;
    [SerializeField]
    private HandleUI handleGUI;
    [SerializeField]
    private Animator scoreAnimator;
    [SerializeField]
    private Animator gameOverAnimator;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private GameObject pauseModal;

    #pragma warning restore CS0649
    #endregion

    private TextMeshProUGUI scoreText;
    private readonly int scoreAnimationHash = Animator.StringToHash("ScoreAnimation");
    private readonly int gameOverHash = Animator.StringToHash("GameOverAnimation");

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

    public void ClosePopups()
    {
        pauseModal.SetActive(false);
    }

    public void ShowPause()
    {
        pauseModal.SetActive(true);
    }

    public void HidePause()
    {
        pauseModal.SetActive(false);
    }

    public void GameOver()
    {
        gameOverAnimator.Play(gameOverHash, 0, 0);
    }
}
