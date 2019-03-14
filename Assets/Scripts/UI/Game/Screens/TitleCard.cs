using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustPingNoPong.UI
{
    public class TitleCard : MonoBehaviour
    {
        public void OnClickOnStart()
        {
            SceneManager.LoadScene("Main");
        }
    }
}