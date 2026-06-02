using Audio;
using Data;
using Player;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Gameplay.Elements.Enemies
{
    /// <summary>
    /// Red virus game object
    /// Block player movements => need to spam key
    /// </summary>
    public class VirusRed: Virus
    {
        [Header("Settings")]
        [SerializeField] private InputActionReference spamKey;
        [SerializeField] private int requiredPressed = 10;
        
        private bool _active;
        private int _count;
        
        protected override void OnApply()
        {
            _count = 0;
            _active = true;
            spamKey.action.started += OnKeyPressed;
            PlayerController.Instance.DisableMovement();
            GameEvents.OnGameFeelProfileStart?.Invoke(gameFeelProfile);
            AlertPanelUI.Instance.ShowPanel(AlertPanelType.Virus, alertTitleText, GetActionText());
            AudioManager.Instance.PlayOneShot(SfxType.AlertRedVoice);
        }

        protected override void OnRemove()
        {
            _active = false;
            PlayerController.Instance.EnableMovement();
            GameEvents.OnGameFeelEnd?.Invoke();
            AlertPanelUI.Instance.HideActivePanel();
            Destroy(gameObject);
        }
        
        private void OnKeyPressed(InputAction.CallbackContext ctx)
        {
            if (!_active) return;
            _count++;
            AlertPanelUI.Instance.SetActionText(GetActionText()); 
            
            if (_count < requiredPressed) return;
            spamKey.action.started -= OnKeyPressed;
            PlayerController.Instance.DetachVirus();
        }
        
        private string GetActionText() => $"{spamKey.action.name} x{(requiredPressed - _count)}";
        private void OnDisable() => spamKey.action.started -= OnKeyPressed;
    }
}