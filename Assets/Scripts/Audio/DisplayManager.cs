using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class DisplayManager : GameBehavior
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
