using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    /// <summary>
    /// Manage the display settings
    /// </summary>
    public class DisplayManager : GameBehavior
    {
        [Header("References")]
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
