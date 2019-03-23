using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JustPingNoPong.UI
{
    public class TitleCard : Screen
    {
#pragma warning disable CS0649 // field is never assigned to
        [SerializeField]
        private Button StartButton;
        [SerializeField]
        private GameObject PlayLabel;
#pragma warning disable IDE0044 // Add readonly modifier
        [SerializeField]
        private float StartButtonDelay;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // field is never assigned to

        private void Awake()
        {
            DisableNextScreenInteraction();
        }

        private void Start()
        {
            Show();
        }

        public override void Show()
        {
            SetActive(true);
            Invoke("EnableNextScreenInteraction", StartButtonDelay);
        }

        public override void Hide()
        {
            DisableNextScreenInteraction();
            SetActive(false);
        }

        public void OnClickOnStart()
        {
            SceneManager.LoadScene(1);
        }

        private void EnableNextScreenInteraction()
        {
            PlayLabel.SetActive(true);
            Invoke("EnableInteraction", 0.3f);
        }

        private void DisableNextScreenInteraction()
        {
            PlayLabel.SetActive(false);
            StartButton.interactable = false;
        }

        private void EnableInteraction()
        {
            StartButton.interactable = true;
        }
    }
}