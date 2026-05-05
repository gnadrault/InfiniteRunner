using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace World.GameElement.Virus.Solution
{
    public class SolutionSpamKey : VirusSolution
    {
        [SerializeField] private InputActionReference spamKey;
        [SerializeField] private int requiredPressed = 10;
        private int _currentPressedCount;
        private PlayerController _player;
        
        private void OnDisable()
        {
            spamKey.action.started -= OnKeyPressed;
        }

        private void OnKeyPressed(InputAction.CallbackContext ctx)
        {
            if (_player == null) return;
            _currentPressedCount++;
            OnCheckSolution();
        }

        public override void OnAttached(PlayerController player)
        {
            _player = player;
            _currentPressedCount = 0;
            spamKey.action.started += OnKeyPressed;
        }

        private void UpdateCountLabel()
        {
            HUD.Instance.UpdateVirusLabel(GetLabel());
        }
        
        public override string GetLabel()
        {
            return $"{spamKey.action.name} x{(requiredPressed - _currentPressedCount)}";
        }

        public override void OnCheckSolution()
        {
            if (_currentPressedCount >= requiredPressed)
            {
                spamKey.action.started -= OnKeyPressed;
                _player.DetachVirus();
            }
            else
            {
                UpdateCountLabel();
            }
        }
    }
}