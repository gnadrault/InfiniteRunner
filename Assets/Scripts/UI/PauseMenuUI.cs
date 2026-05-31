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
        }

        private void OnDisable()
        {
            pauseAction.action.performed -= OnPausePressed;
        }

        private void OnPausePressed(InputAction.CallbackContext obj)
        {
            TogglePause(!TimeManager.IsPaused);
        }
        
        public void TogglePause(bool requestPause)
        {
            TimeManager.Instance.SetPaused(requestPause);
            pauseCanvas.SetActive(requestPause);
        }
    }
}