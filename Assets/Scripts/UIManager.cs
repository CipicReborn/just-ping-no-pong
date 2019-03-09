
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649

    [SerializeField]
    private TextMeshProUGUI ScoreTextGUI;
    [SerializeField]
    private HandleUI HandleGUI;
    [SerializeField]
    private Animator ScoreAnimator;
    [SerializeField]
    private Animator GameOverAnimator;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private GameObject PauseModal;
    [Header("Tips")]
    [SerializeField]
    private GameObject TipsCanvas;
    [SerializeField]
    private GameObject TipsBasic;

#pragma warning restore CS0649
    #endregion

    private TextMeshProUGUI scoreText;
    private readonly int scoreAnimationHash = Animator.StringToHash("ScoreAnimation");
    private readonly int gameOverHash = Animator.StringToHash("GameOverAnimation");

    private void Awake()
    {
        scoreText = ScoreAnimator.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreGUI(int scored, int totalScore, Vector3 worldPosition)
    {
        ScoreTextGUI.text = totalScore.ToString();
        if (scored > 0)
        {
            ScoreAnimator.transform.position = Camera.WorldToScreenPoint(worldPosition);
            scoreText.text = string.Format("+{0}", scored);
            ScoreAnimator.Play(scoreAnimationHash, 0, 0);
        }
    }

    public void UpdatePadGUI(float normalisedPadPosition)
    {
        HandleGUI.UpdatePadUIFeedback(normalisedPadPosition);
    }

    public void ClosePopups()
    {
        PauseModal.SetActive(false);
    }

    public void ShowPause()
    {
        PauseModal.SetActive(true);
    }

    public void HidePause()
    {
        PauseModal.SetActive(false);
    }

    public void ShowTips()
    {
        TipsBasic.SetActive(true);
        TipsCanvas.SetActive(true);
    }

    public void HideTips()
    {
        TipsCanvas.SetActive(false);
    }

    public void ShowMission()
    {
        
    }
    public void GameOver()
    {
        GameOverAnimator.Play(gameOverHash, 0, 0);
    }
}
