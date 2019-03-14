using UnityEngine;

namespace JustPingNoPong.UI
{
    public abstract class Screen : MonoBehaviour
    {
        public UIManager UIManager { protected get; set; }

        public abstract void Show();

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}