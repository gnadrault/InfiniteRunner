using Gameplay;
using UnityEngine;

namespace Core
{
    public abstract class GameBehavior : MonoBehaviour
    {
        private void Update()
        {
            AlwaysUpdate();
            if (TimeManager.IsPaused) return;
            GameplayUpdate();
        }
        
        protected virtual void AlwaysUpdate() {}

        protected virtual void GameplayUpdate() {}
    }
}