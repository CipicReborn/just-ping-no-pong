using UnityEngine;

namespace JustPingNoPong.UI
{
    public class Menu : Window
    {
        public override void Show()
        {
            SetActive(true);
            Debug.LogWarning("Restart Menu WIP");
        }

        public override void Hide()
        {
            SetActive(false);
        }

        public void OnClickOnQuit()
        {
            UIManager.Quit();
        }

        public void OnClickOnRestart()
        {
            UIManager.ResetGame();
        }
    }
}