using UnityEngine;
using Utils;

namespace Player.State
{
    public class JumpingState : MonoBehaviour, IPlayerState
    {
        [Header("Settings")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float timeToApex = 0.1f; // Time to reach the Apex
        [SerializeField] private float timeApexWait = 0.2f; // Time waiting at Apex (1 input)
        [SerializeField] private float timeApexExtraWait = 0.2f; // Time waiting at Apex (keep pressed)
        [SerializeField] private float timeDescent = 0.2f; // Time to descent from Apex 
        
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

        private float _apexStartTime;
        private float _shortApexEndTime;
        private float _longApexEndTime;

        public void Enter()
        {
            _currentPhase = JumpPhase.Ascending;
            _elapsedTime = 0f;
            _descentElapsedTime = 0f;
            _startY = PlayerController.Instance.GetCurrentPosition().y;

            // Calculate phase timing
            _apexStartTime = timeToApex;
            _shortApexEndTime = _apexStartTime + timeApexWait;
            _longApexEndTime = _shortApexEndTime + timeApexExtraWait;
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

            float newY = _startY + heightFactor * jumpHeight;
            PlayerController.Instance.SetPositionY(newY);
        }
        
        private float UpdateAscending()
        {
            if (_elapsedTime >= _apexStartTime)
            {
                _currentPhase = JumpPhase.Apex;
                return 1f;
            }

            float t = _elapsedTime / timeToApex;
            return EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
        }

        private float UpdateApex()
        {
            bool shortApexExpired = _elapsedTime >= _shortApexEndTime;
            bool longApexExpired = _elapsedTime >= _longApexEndTime;
            bool jumpButtonHeld = PlayerController.Instance.IsJumpButtonPressed();

            if ((shortApexExpired && !jumpButtonHeld) || longApexExpired) // Waiting in apex (short or long)
            {
                _currentPhase = JumpPhase.Descending;
            }
            return 1f;
        }

        private float UpdateDescending()
        {
            _descentElapsedTime += Time.deltaTime;

            float t = _descentElapsedTime / timeDescent;
            return EasingFunctions.EaseInQuint(Mathf.Clamp01(t));
        }

        public void Exit()
        {
            
        }

        public bool IsDone()
        {
            return _currentPhase == JumpPhase.Descending
                   && _descentElapsedTime >= timeDescent;
        }
    }
}
