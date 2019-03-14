using UnityEngine;

namespace JustPingNoPong.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        protected UIManager UIManager;

        public void Init(UIManager um)
        {
            UIManager = um;
        }

        public abstract void Show();

        public abstract void Hide();

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}