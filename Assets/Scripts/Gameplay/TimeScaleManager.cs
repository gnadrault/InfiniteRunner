using UnityEngine;

namespace Gameplay
{
    public class TimeScaleManager : MonoBehaviour
    {
        public static TimeScaleManager Instance;
        private float _initTimeScale;

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
    }
}