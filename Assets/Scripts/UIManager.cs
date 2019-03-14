using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustPingNoPong.UI
{
    public class UIManager : MonoBehaviour
    {
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649

        [Header("In Game UI")]
        [SerializeField]
        private HUD HUD;

        [Header("Screens")]
        [SerializeField]
        private TipsScreen TipsScreen;
        [Header("Windows")]
        [SerializeField]
        private MissionsUIController MissionCanvas;
        [SerializeField]
        private GameObject PauseModal;
        [SerializeField]
        private GameObject MenuModal;

#pragma warning restore CS0649
        #endregion

        
        private IGameManager gameManager;

        private void Awake()
        {

            Reset();
        }

        public void Init(GameManager gm)
        {
            gameManager = gm;
            HUD.Init(gm);
            MissionCanvas.Init(gm, this);
            TipsScreen.UIManager = this;
        }

        public void ShowPause()
        {
            PauseModal.SetActive(true);
        }

        private void HidePause()
        {
            PauseModal.SetActive(false);
        }

        public void Quit()
        {
            SceneManager.LoadScene("Menu");
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

        public void Reset()
        {
            HUD.Reset();
            ClosePopups();
            HideMission();
            HideResults();
            TipsScreen.Hide();
        }
    }
}