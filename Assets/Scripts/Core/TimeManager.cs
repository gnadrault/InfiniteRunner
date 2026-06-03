using Audio;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Global time manager to update the timescale
    /// 
    /// !!! Timescale should only be set here
    /// </summary>
    public class TimeManager : GameBehavior
    {
        public static TimeManager Instance;
        private float _gameplayTimeScale = 1f;
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
            AudioManager.Instance.SetMusicPitch(Time.timeScale);
        }
    }
}