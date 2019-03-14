using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustPingNoPong.UI
{
    public class MissionWindow : Window
    {
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044 // Disable recommendation "Add readonly modifier"   [SerializeField]

        [SerializeField]
        private GameObject NextButton;

        [SerializeField]
        private TextMeshProUGUI MissionText;
        [SerializeField]
        private Image RewardImage;
        [SerializeField]
        private TextMeshProUGUI RewardNameText;

#pragma warning restore IDE0044
#pragma warning restore CS0649
        #endregion

        private GameManager gameManager;
        private AudioSource winSound;

        private void Awake()
        {
            winSound = GetComponent<AudioSource>();
        }

        public void Init(GameManager gm, UIManager um)
        {
            Init(um);
            gameManager = gm;
        }

        public void ShowMission(Mission mission)
        {
            MissionText.text = mission.DescriptionText;
            RewardImage.sprite = mission.Reward.Image;
            RewardNameText.text = mission.Reward.Name;
            NextButton.SetActive(true);
            Show();
        }

        public void OnClickOnNext()
        {
            Hide();
            Debug.Log("Calling ResetGame");
            gameManager.ResetGame();
        }

        public override void Show()
        {
            SetActive(true);
        }

        public override void Hide()
        {
            SetActive(false);
        }
    }
}