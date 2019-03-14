using TMPro;
using UnityEngine;

namespace JustPingNoPong.UI
{
    public class HUD : UIElement
    {
        
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Field is never assigned to

        [SerializeField]
        private TextMeshProUGUI ScoreBoardGUI;
        [SerializeField]
        private HandleUI HandleGUI;
        [SerializeField]
        private Camera Camera;

        [Header("FlyingFeedbacks")]
        [SerializeField]
        private Effect ScoreFeedback;
        [SerializeField]
        private Effect SuccessFeedback;
        [SerializeField]
        private Effect GameOverFeedback;

        #pragma warning restore CS0649
        #endregion

        private TextMeshProUGUI scoreText;
        private IGameManager gameManager;

        #region UNITY INITIALISATION

        void Awake()
        {
            SuccessFeedback.gameObject.SetActive(true);
            GameOverFeedback.gameObject.SetActive(true);
            ScoreFeedback.gameObject.SetActive(true);
            scoreText = ScoreFeedback.GetComponentInChildren<TextMeshProUGUI>();
        }

        #endregion


        #region API

        public void Init(IGameManager gm)
        {
            gameManager = gm;
        }

        public override void Show()
        {
            SetActive(true);
        }

        public override void Hide()
        {
            SetActive(false);
        }

        public void Reset()
        {
            UpdateScoreGUI(0, 0, Vector3.zero);
            UpdatePadGUI(0.5f);
        }

        public void UpdateScoreGUI(int scored, int totalScore, Vector3 worldPosition)
        {
            ScoreBoardGUI.text = totalScore.ToString();
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

        public void ShowMissionSuccessFeedback()
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



        #endregion
    }
}