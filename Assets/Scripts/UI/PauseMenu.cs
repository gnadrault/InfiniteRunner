using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private InputActionReference pauseAction;
        [SerializeField] private GameObject pauseCanvas;

        private bool _isPaused;

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
            if (_isPaused) Resume();
            else Pause();
        }


        public void Pause()
        {
            _isPaused = true;
            GameEvents.OnPause?.Invoke();
            TimeScaleManager.Instance.SetTimeScale(0f);
            pauseCanvas.SetActive(true);
        }

        public void Resume()
        {
            _isPaused = false;
            GameEvents.OnResume?.Invoke();
            TimeScaleManager.Instance.SetTimeScale(1f);
            pauseCanvas.SetActive(false);
        }
    }
}