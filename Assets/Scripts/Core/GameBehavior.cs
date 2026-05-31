using Gameplay;
using UnityEngine;

namespace Core
{
    public abstract class GameBehavior : MonoBehaviour
    {
        private void Update()
        {
            AlwaysUpdate();
            if (GameStateManager.Instance.State != GameState.Gameplay) return;
            GameplayUpdate();
        }
        
        protected virtual void AlwaysUpdate() {}

        protected virtual void GameplayUpdate() {}
    }
}