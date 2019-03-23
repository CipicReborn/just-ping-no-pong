
namespace JustPingNoPong.UI
{
    public class Pause : Window
    {
        public override void Show()
        {
            SetActive(true);
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

        public void OnClickOnResume()
        {
            UIManager.ResumeGame();
        }

        public void OnClickOnCheat()
        {
            Hide();
            UIManager.ShowCheatPanel();
        }
    }
}
