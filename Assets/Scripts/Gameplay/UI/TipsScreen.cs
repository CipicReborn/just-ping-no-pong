﻿using UnityEngine;
using UnityEngine.UI;

namespace JustPingNoPong.UI
{
    public class TipsScreen : Screen
    {
        #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044

        [SerializeField]
        private GameObject TipsBasic;

        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private float MinimumDisplayTime;

#pragma warning restore IDE0044
#pragma warning restore CS0649
        #endregion

        public override void Show()
        {
            TipsBasic.SetActive(true);
            gameObject.SetActive(true);
        }

        public void OnEnable()
        {
            NextButton.interactable = false;
            Invoke("EnableNext", MinimumDisplayTime);
        }

        public void EnableNext()
        {
            NextButton.interactable = true;
        }

        public void OnClickOnNext()
        {
            gameObject.SetActive(false);
            UIManager.CloseTipsAndProceed();
        }


    }
}