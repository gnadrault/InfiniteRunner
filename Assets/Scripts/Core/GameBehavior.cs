using UnityEngine;
using Utils;

namespace Core
{
    public abstract class GameBehavior : MonoBehaviour
    {
        private bool _isPaused;

        private void OnEnable()
        {
            GameEvents.OnPause += HandlePause;
            GameEvents.OnResume += HandleResume;
        }
        
        private void OnDisable()
        {
            GameEvents.OnPause -= HandlePause;
            GameEvents.OnResume -= HandleResume;
        }

        private void HandlePause() => _isPaused = true;
        private void HandleResume() => _isPaused = false;

        private void Update()
        {
            AlwaysUpdate();
            if (_isPaused) return;
            GameplayUpdate();
        }
        
        protected virtual void AlwaysUpdate() {}

        protected virtual void GameplayUpdate() {}
    }
}