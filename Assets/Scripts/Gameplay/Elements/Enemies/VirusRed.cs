using Player;
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
        private int _currentPressedCount;
        private PlayerController _player;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            _currentPressedCount = 0;
            spamKey.action.started += OnKeyPressed;
            player.DisableMovement();
            GameEvents.OnGameFeelProfile?.Invoke(gameFeelProfile);
            HUD.Instance.ShowVirusPanel(GetHUDLabel());
        }

        public override void RemoveEffect(PlayerController player)
        {
            player.EnableMovement();
            GameEvents.OnGameFeelEnd?.Invoke();
            HUD.Instance.HideVirusPanel();
            Destroy(gameObject);
        }
        
        #region Solution
        private void OnKeyPressed(InputAction.CallbackContext ctx)
        {
            if (_player == null) return;
            _currentPressedCount++;
            OnCheckSolution();
        }
        
        private void OnCheckSolution()
        {
            if (_currentPressedCount >= requiredPressed)
            {
                spamKey.action.started -= OnKeyPressed;
                _player.DetachVirus();
            }
            else
            {
                HUD.Instance.UpdateVirusLabel(GetHUDLabel());
            }
        }
        
        private string GetHUDLabel()
        {
            return $"{spamKey.action.name} x{(requiredPressed - _currentPressedCount)}";
        }
        
        private void OnDisable()
        {
            spamKey.action.started -= OnKeyPressed;
        }
        #endregion
    }
}