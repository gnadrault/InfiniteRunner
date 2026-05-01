using Player.Data;
using UnityEngine;
using Utils;

namespace Player.State
{
    public class JumpingState : IPlayerState
    {
        private readonly PlayerController _playerController;
        private readonly JumpSettings _jumpSettings;
        
        private enum JumpPhase
        {
            Ascending,
            Apex,
            Descending
        }

        private JumpPhase _currentPhase;
        private float _elapsedTime;
        private float _descentElapsedTime;
        private float _startY;

        // Apex timing
        private float _apexStartTime;
        private float _shortApexEndTime;
        private float _longApexEndTime;

        public JumpingState(PlayerController playerController, JumpSettings jumpSettings)
        {
            _playerController = playerController;
            _jumpSettings = jumpSettings;
        }

        public void Enter()
        {
            _currentPhase = JumpPhase.Ascending;
            _elapsedTime = 0f;
            _descentElapsedTime = 0f;
            _startY = _playerController.GetCurrentPosition().y;

            // Calculate phase timing
            _apexStartTime = _jumpSettings.timeToApex;
            _shortApexEndTime = _apexStartTime + _jumpSettings.timeApexWait;
            _longApexEndTime = _shortApexEndTime + _jumpSettings.timeApexExtraWait;
        }
        
        /// <summary>
        /// Jump is separated into 3 phases:
        /// 1. Ascending: Jump up to apex
        /// 2. Apex: Wait at peak (duration depends on button press)
        /// 3. Descending: Fall down
        /// </summary>
        public void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            float heightFactor = 0f;
            switch (_currentPhase)
            {
                case JumpPhase.Ascending:
                    heightFactor = UpdateAscending();
                    break;
                case JumpPhase.Apex:
                    heightFactor = UpdateApex();
                    break;
                case JumpPhase.Descending:
                    heightFactor = UpdateDescending();
                    break;
            }

            float newY = _startY +  _jumpSettings.jumpHeight * heightFactor;
            _playerController.SetPositionY(newY);
        }
        
        private float UpdateAscending()
        {
            if (_elapsedTime >= _apexStartTime)
            {
                _currentPhase = JumpPhase.Apex;
                return 1f;
            }

            float t = _elapsedTime / _jumpSettings.timeToApex;
            return EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
        }

        private float UpdateApex()
        {
            bool shortApexExpired = _elapsedTime >= _shortApexEndTime;
            bool longApexExpired = _elapsedTime >= _longApexEndTime;
            bool jumpButtonHeld = _playerController.IsJumpButtonPressed();

            if ((shortApexExpired && !jumpButtonHeld) || longApexExpired) // Waiting in apex (short or long)
            {
                _currentPhase = JumpPhase.Descending;
            }
            return 1f;
        }

        private float UpdateDescending()
        {
            _descentElapsedTime += Time.deltaTime;

            float t = _descentElapsedTime / _jumpSettings.timeDescent;
            return EasingFunctions.EaseInQuint(Mathf.Clamp01(t));
        }

        public void Exit()
        {
            
        }

        public bool IsDone()
        {
            return _currentPhase == JumpPhase.Descending
                   && _descentElapsedTime >= _jumpSettings.timeDescent;
        }
    }
}
