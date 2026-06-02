using UnityEngine;

namespace Core
{
    /// <summary>
    /// Custom MonoBehavior to manage updates order and pause/resume gameplay 
    /// </summary>
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