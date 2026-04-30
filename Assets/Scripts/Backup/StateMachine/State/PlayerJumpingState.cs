using UnityEngine;
using Utils;

namespace Backup.StateMachine.State
{
    public class PlayerJumpingState : PlayerBaseState
    {
        // Settings
        private readonly float _timeToApex;
        private readonly float _timeApexWait;
        private readonly float _timeApexExtraWait;
        private readonly float _timeDescent;
        private readonly float _jumpHeight;

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

        public PlayerJumpingState(
            PlayerStateMachine stateMachine,
            PlayerController controller,
            float timeToApex,
            float timeApexWait,
            float timeApexExtraWait,
            float timeDescent,
            float jumpHeight)
            : base(stateMachine, controller)
        {
            _timeToApex = timeToApex;
            _timeApexWait = timeApexWait;
            _timeApexExtraWait = timeApexExtraWait;
            _timeDescent = timeDescent;
            _jumpHeight = jumpHeight;
        }

        public override void Enter()
        {
            base.Enter();

            _currentPhase = JumpPhase.Ascending;
            _elapsedTime = 0f;
            _descentElapsedTime = 0f;
            _startY = Controller.GetCurrentPosition().y;

            // Calculate phase timing
            _apexStartTime = _timeToApex;
            _shortApexEndTime = _apexStartTime + _timeApexWait;
            _longApexEndTime = _shortApexEndTime + _timeApexExtraWait;
        }

        public override void Update()
        {
            UpdateJump();
            base.Update();
        }

        /// <summary>
        /// Jump is separated into 3 phases:
        /// 1. Ascending: Jump up to apex
        /// 2. Apex: Wait at peak (duration depends on button press)
        /// 3. Descending: Fall down
        /// </summary>
        public void UpdateJump()
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

            float newY = _startY + heightFactor * _jumpHeight;
            Controller.SetPositionY(newY);
        }

        private float UpdateAscending()
        {
            if (_elapsedTime >= _apexStartTime)
            {
                _currentPhase = JumpPhase.Apex;
                return 1f;
            }

            float t = _elapsedTime / _timeToApex;
            return EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
        }

        private float UpdateApex()
        {
            bool shortApexExpired = _elapsedTime >= _shortApexEndTime;
            bool longApexExpired = _elapsedTime >= _longApexEndTime;
            bool jumpButtonHeld = Controller.IsJumpButtonPressed();

            if ((shortApexExpired && !jumpButtonHeld) || longApexExpired) // Waiting in apex (short or long)
            {
                _currentPhase = JumpPhase.Descending;
            }
            return 1f;
        }

        private float UpdateDescending()
        {
            _descentElapsedTime += Time.deltaTime;

            float t = _descentElapsedTime / _timeDescent;
            return EasingFunctions.EaseInQuint(Mathf.Clamp01(t));
        }

        protected override void CheckTransitions()
        {
            if (_currentPhase == JumpPhase.Descending && _descentElapsedTime >= _timeDescent)
            {
                StateMachine.ChangeState(Controller.IdleState);
            }
        }
    }
}