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
        private MissionWindow MissionWindow;
        [SerializeField]
        private ResultsWindow ResultsWindow;
        [SerializeField]
        private Pause PausePopup;
        [SerializeField]
        private Menu MenuPopup;

#pragma warning restore CS0649
        #endregion


        #region UNITY INITIALISATION

        private void Awake()
        {
        }

        private void Start()
        {
            //ResetGame();
            HUD.Hide();
            CloseWindows();
            TipsScreen.Show();
        }

        #endregion 


        #region API

        public void Init(GameManager gm)
        {
            gameManager = gm;
            HUD.Init(gm);
            MissionWindow.Init(gm, this);
            TipsScreen.Init(this);
            PausePopup.Init(this);
            MenuPopup.Init(this);
            MissionWindow.Init(this);
            ResultsWindow.Init(this);
        }

        public void CloseTipsAndProceed()
        {
            HUD.Show();
            MissionWindow.ShowMission(gameManager.CurrentMission);
        }

        public void OnClickOnPause()
        {
            PausePopup.Show();
            gameManager.PauseGame();
        }

        public void ResumeGame()
        {
            CloseWindows();

            gameManager.ResumeGame();
        }

        public void ShowResults(Mission mission, int score, bool success)
        {
            ResultsWindow.ShowResults(mission, score, success);
        }

        public void ShowMenu()
        {
            MenuPopup.Show();
        }

        public void ResetGame()
        {
            HUD.Reset();
            CloseWindows();
            MissionWindow.Hide();
            ResultsWindow.Hide();
            TipsScreen.Hide();

            gameManager.ResetGame();
        }

        public void Quit()
        {
            SceneManager.LoadScene("Menu");
        }

        #endregion


        #region IMPLEMENTATION

        private IGameManager gameManager;


        private void CloseWindows()
        {
            PausePopup.Hide();
            MenuPopup.Hide();
            MissionWindow.Hide();
            ResultsWindow.Hide();
        }

        #endregion
    }
}