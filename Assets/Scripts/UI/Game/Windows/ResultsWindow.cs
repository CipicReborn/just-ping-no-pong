using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustPingNoPong.UI
{
    public class ResultsWindow : Window
    {
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044 // Disable recommendation "Add readonly modifier"   [SerializeField]

        [SerializeField]
        private GameObject NextButton;

        [SerializeField]
        private TextMeshProUGUI MissionReminderText;
        [SerializeField]
        private TextMeshProUGUI ResultText;
        [SerializeField]
        private GameObject RewardBlock;
        [SerializeField]
        private GameObject TryAgainBlock;
        [SerializeField]
        private TextMeshProUGUI UnlockedItemName;
        [SerializeField]
        private Image UnlockedItemImage;

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
            UIManager = um;
        }

        public void ShowResults(Mission mission, int finalScore, bool success)
        {
            Show();

            MissionReminderText.text = "Target: " + mission.ReminderText;
            ResultText.text = "Your score: <color=#" + (success ? "00ff" : "ff00") + "00>" + finalScore.ToString() + "</color>";

            if (success)
            {
                //RewardBlock update
                UnlockedItemName.text = "You unlocked " + mission.Reward.Name;
                UnlockedItemImage.sprite = mission.Reward.Image;
                winSound.Play();
            }

            RewardBlock.SetActive(success);
            TryAgainBlock.SetActive(!success);
            NextButton.SetActive(true);
        }

        public void OnClickOnNext()
        {
            Hide();
            UIManager.ShowMenu();
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