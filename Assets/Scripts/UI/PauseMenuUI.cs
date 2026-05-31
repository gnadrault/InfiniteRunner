using Core;
using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI
{
    public class PauseMenuUI : GameBehavior
    {
        [SerializeField] private InputActionReference pauseAction;
        [SerializeField] private GameObject pauseCanvas;

        private void OnEnable()
        {
            pauseAction.action.performed += OnPausePressed;
            GameEvents.OnGameStateChanged += HandleState;
        }

        private void OnDisable()
        {
            pauseAction.action.performed -= OnPausePressed;
            GameEvents.OnGameStateChanged -= HandleState;
        }

        private void OnPausePressed(InputAction.CallbackContext obj)
        {
            GameState current = GameStateManager.Instance.State;
            if (current == GameState.GameOver) return;

            GameState next = current == GameState.Paused ? GameState.Gameplay : GameState.Paused;
            GameStateManager.Instance.SetState(next);
        }
        
        private void HandleState(GameState state)
        {
            pauseCanvas.SetActive(state == GameState.Paused);
        }

        public void Resume()
        {
            GameStateManager.Instance.SetState(GameState.Gameplay);
        }
    }
}