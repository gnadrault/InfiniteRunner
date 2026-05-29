using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField] private Toggle fullscreenToggle;

        private void Start()
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }

        public void OnFullscreenChanged(bool isFullscreen)
        {
            Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }
    }
}
