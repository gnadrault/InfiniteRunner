using Data;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public class VirusRed: Virus
    {
        [SerializeField] private InputActionReference spamKey;
        [SerializeField] private int requiredPressed = 10;
        
        private int _count;
        
        protected override void OnApply()
        {
            _count = 0;
            spamKey.action.started += OnKeyPressed;
            player.DisableMovement();
            GameEvents.OnGameFeelProfileStart?.Invoke(gameFeelProfile);
            AlertPanelUI.Instance.ShowPanel(AlertPanelType.Virus, alertTitleText, GetActionText());
        }

        protected override void OnRemove()
        {
            player.EnableMovement();
            GameEvents.OnGameFeelEnd?.Invoke();
            AlertPanelUI.Instance.HideActivePanel();
            Destroy(gameObject);
        }
        
        private void OnKeyPressed(InputAction.CallbackContext ctx)
        {
            if (player == null) return;
            _count++;
            AlertPanelUI.Instance.SetActionText(GetActionText()); 
            
            if (_count < requiredPressed) return;
            spamKey.action.started -= OnKeyPressed;
            player.DetachVirus();
        }
        
        private string GetActionText() => $"{spamKey.action.name} x{(requiredPressed - _count)}";
        private void OnDisable() => spamKey.action.started -= OnKeyPressed;
    }
}