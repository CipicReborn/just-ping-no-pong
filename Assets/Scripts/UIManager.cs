using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustPingNoPong.UI
{
    public class UIManager : MonoBehaviour
    {
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649

        [Header("In Game UI")]
        [SerializeField]
        private TextMeshProUGUI ScoreTextGUI;
        [SerializeField]
        private HandleUI HandleGUI;
        [SerializeField]
        private Effect ScoreFeedback;
        [SerializeField]
        private Effect GameOverFeedback;
        [SerializeField]
        private Effect SuccessFeedback;
        [SerializeField]
        private Camera Camera;
        [SerializeField]
        private GameObject PauseModal;
        [SerializeField]
        private GameObject MenuModal;
        [Header("Tips")]
        [SerializeField]
        private TipsScreen TipsScreen;
        [Header("Missions")]
        [SerializeField]
        private MissionsUIController MissionCanvas;

#pragma warning restore CS0649
        #endregion

        private TextMeshProUGUI scoreText;
        private IGameManager gameManager;

        private void Awake()
        {
            ScoreFeedback.gameObject.SetActive(true);
            GameOverFeedback.gameObject.SetActive(true);
            SuccessFeedback.gameObject.SetActive(true);
            scoreText = ScoreFeedback.GetComponentInChildren<TextMeshProUGUI>();
            Reset();
        }

        public void Init(GameManager gm)
        {
            gameManager = gm;
            MissionCanvas.Init(gm, this);
            TipsScreen.UIManager = this;
        }

        public void UpdateScoreGUI(int scored, int totalScore, Vector3 worldPosition)
        {
            ScoreTextGUI.text = totalScore.ToString();
            if (scored > 0)
            {
                ScoreFeedback.transform.position = Camera.WorldToScreenPoint(worldPosition);
                scoreText.text = string.Format("+{0}", scored);
                ScoreFeedback.Play();
            }
        }

        public void UpdatePadGUI(float normalisedPadPosition)
        {
            HandleGUI.UpdatePadUIFeedback(normalisedPadPosition);
        }

        public void ShowPause()
        {
            PauseModal.SetActive(true);
        }

        private void HidePause()
        {
            PauseModal.SetActive(false);
        }

        public void ShowMenu()
        {
            MenuModal.SetActive(true);
            Debug.LogWarning("Restart Menu WIP");
        }

        private void HideMenu()
        {
            MenuModal.SetActive(false);
        }

        public void ClosePopups()
        {
            PauseModal.SetActive(false);
            MenuModal.SetActive(false);
        }

        public void ShowTips()
        {
            TipsScreen.Show();
        }

        public void CloseTipsAndProceed()
        {
            ShowMission();
        }

        public void ShowMission()
        {
            MissionCanvas.ShowMission(gameManager.CurrentMission);
        }

        public void HideMission()
        {
            MissionCanvas.ClosePanels();
        }

        public void ShowResults(Mission mission, int score, bool success)
        {
            MissionCanvas.ShowResults(mission, score, success);
        }

        public void HideResults()
        {
            MissionCanvas.ClosePanels();
        }

        public void ShowTargetReachedFeedback()
        {
            SuccessFeedback.Play();
        }

        public void GameOver()
        {
            GameOverFeedback.Play();
            Invoke("OnGameOverEffectComplete", 3f);
        }

        public void OnGameOverEffectComplete()
        {
            gameManager.ProcessResults();
        }

        public void Reset()
        {
            UpdateScoreGUI(0, 0, Vector3.zero);
            UpdatePadGUI(0.5f);
            ClosePopups();
            HideMission();
            HideResults();
            TipsScreen.Hide();
        }
    }
}