using System;
using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private InputActionReference pauseAction;
        [SerializeField] private GameObject endCanvas;

        private void OnEnable()
        {
            GameEvents.OnEndGame += EndGame;
        }
        
        private void OnDisable()
        {
            GameEvents.OnEndGame -= EndGame;
        }

        private void EndGame()
        {
            TimeScaleManager.Instance.SetTimeScale(0f);
            endCanvas.SetActive(true);
        }
    }
}