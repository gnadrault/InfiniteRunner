using UnityEngine;

namespace Core
{
    public class TimeManager : GameBehavior
    {
        public static TimeManager Instance;
        private float _initTimeScale;
        
        public static bool IsPaused { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void SetTimeScale(float scale)
        {
            _initTimeScale = Time.timeScale;
            Time.timeScale = scale;
        }
        
        public void ResetTimeScale()
        {
            Time.timeScale = _initTimeScale;
        }

        public void SetPaused(bool paused)
        {
            IsPaused = paused;
            Time.timeScale = paused ? 0f : 1f;
        }
    }
}