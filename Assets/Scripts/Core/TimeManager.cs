using UnityEngine;

namespace Core
{
    public class TimeManager : GameBehavior
    {
        public static TimeManager Instance;
        private float _gameplayTimeScale;
        private bool _isPaused;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void SetGameplayTimeScale(float scale)
        {
            _gameplayTimeScale = scale;
            ApplyTimeScale();
        }
        
        public void SetPaused(bool paused)
        {
            _isPaused = paused;
            ApplyTimeScale();
        }
        
        private void ApplyTimeScale()
        {
            Time.timeScale = _isPaused ? 0f : _gameplayTimeScale;
        }
    }
}