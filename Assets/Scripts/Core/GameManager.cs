using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public GameState State { get; private set; }
        
        private void OnEnable()
        {
            GameEvents.OnGameStateChanged += HandleState;
        }
        

        private void OnDisable()
        {
            GameEvents.OnGameStateChanged -= HandleState;
        }

        /// <summary>
        /// Init the game with Gameplay state
        /// </summary>
        public void Start()
        {
            GameEvents.OnGameStateChanged.Invoke(GameState.Gameplay);
        }

        /// <summary>
        /// Update the game state
        /// </summary>
        public void SetGameState(GameState newState)
        {
            State = newState;
            GameEvents.OnGameStateChanged.Invoke(newState);
        }

        /// <summary>
        /// Open/Close Pause menu
        /// </summary>
        private void TooglePauseMenu(InputAction.CallbackContext obj)
        {
            switch (State)
            {
                case GameState.Pause:
                    SetGameState(GameState.Gameplay);
                    break;
                case GameState.Gameplay:
                    SetGameState(GameState.Pause);
                    break;
            }
        }

        /// <summary>
        /// Handle new game state event received
        /// </summary>
        /// <param name="state"></param>
        private void HandleState(GameState state)
        {
            switch (state)
            {
                case GameState.Gameplay: // Unload Pause/End menus if present
                    break;
                case GameState.Pause: // Load Pause menu
                    break;
                case GameState.End: // Load End menu
                    break;
            }
        }
    }
}