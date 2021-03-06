﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustPingNoPong.UI
{
    public class UIManager : MonoBehaviour
    {

        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649
        [SerializeField]
        private Camera Camera;

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
        [SerializeField]
        private PadSelectionWindow PadSelection;
        [SerializeField]
        private CheatPanel CheatPanel;
        [Header("Balls")]
        public List<Ball> Balls;
        //public List<BallInfo> BallInfos;
        [Header("Pads")]
        public List<Pad> Pads;
        public List<PadInfo> Infos;
#pragma warning restore CS0649
        #endregion
        private Animator cameraman;

        #region UNITY INITIALISATION

        private void Awake()
        {
            cameraman = Camera.GetComponent<Animator>();
        }

        private void Start()
        {
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
            PadSelection.Init(this);
            CheatPanel.Init(this);
        }

        public void CloseTipsAndProceed()
        {
            HUD.Show();
            MissionWindow.ShowMission(gameManager.CurrentMission);
        }

        int padIndex = 0;
        int ballIndex = 0;

        public int GetNextPadId()
        {
            return ++padIndex;
        }

        public int GetPrevPadId()
        {
            return --padIndex;
        }
        public PadInfo GetPadInfo(int i)
        {
            return Infos[i];
        }

        public void FocusPad(int index)
        {
            padIndex = index;
            for (int i = 0; i < Pads.Count; i++)
            {
                Pads[i].gameObject.SetActive(padIndex == i);
            }
        }
        public void FocusBall(int index)
        {
            ballIndex = index;
            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].gameObject.SetActive(ballIndex == i);
            }
        }
        public void EquipPad(int i)
        {
            gameManager.EquipPad(Pads[i]);
            PadSelection.Hide();
            cameraman.SetTrigger("CameraMain");
            HUD.Show();
            MissionWindow.ShowMission(gameManager.CurrentMission);
        }

        public void EquipBall(int i)
        {
            gameManager.EquipBall(Balls[i]);
            //BallSelection.Hide();
            cameraman.SetTrigger("CameraMain");
            HUD.Show();
            MissionWindow.ShowMission(gameManager.CurrentMission);
        }

        public void OnClickOnPause()
        {
            if (gameManager.GameIsOver) return;
            PausePopup.Show();
            gameManager.PauseGame();
        }

        public void ResumeGame()
        {
            CloseWindows();

            gameManager.ResumeGame();
        }


        public void ShowCheatPanel()
        {
            CheatPanel.Show();
        }

        public void ShowResults(Mission mission, int score, bool success)
        {
            ResultsWindow.ShowResults(mission, score, success);
        }

        public void ShowMenu()
        {
            MenuPopup.Show();
        }

        public void ShowEquipment()
        {
            HUD.Hide();
            CloseWindows();
            cameraman.SetTrigger("CameraPad");
            PadSelection.Show();
        }

        public void ResetGame()
        {
            HUD.Reset();
            CloseWindows();
            TipsScreen.Hide();

            gameManager.ResetGame();
        }

        public void Quit()
        {
            SceneManager.LoadScene(0);
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
            PadSelection.Hide();
            CheatPanel.Hide();
        }

        #endregion
    }
}