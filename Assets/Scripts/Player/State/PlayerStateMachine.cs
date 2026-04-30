using System;
using Player.Data;

namespace Player.State
{
    public class PlayerStateMachine
    {
        private LaneChangingState _laneChangingState;
        private JumpingState _jumpingState;
        private IdleState _idleState;
        
        private enum PlayerState
        {
            Idle,
            ChangingLane,
            Jumping
        }
        
        private PlayerState _currentStateValue;
        private IPlayerState _currentStateHandler;

        public PlayerStateMachine(PlayerController playerController, PlayerSettings playerSettings)
        {
            _idleState = new IdleState();
            _jumpingState = new JumpingState(playerController, playerSettings.jump);
            _laneChangingState = new LaneChangingState(playerController, playerSettings.changeLane);
        }
        
        public void InitializeState()
        {
            SetState(PlayerState.Idle);
            _currentStateHandler.Enter();
        }
        
        public void UpdateState()
        {
            CheckStateTransitions();
            _currentStateHandler.UpdateState();
        }
        
        private void ChangeState(PlayerState newPlayerState)
        {
            _currentStateHandler.Exit();
            SetState(newPlayerState);
            _currentStateHandler.Enter();
        }

        private void SetState(PlayerState newPlayerState)
        {
            _currentStateValue = newPlayerState;
            _currentStateHandler = _currentStateValue switch
            {
                PlayerState.Idle => _idleState,
                PlayerState.ChangingLane => _laneChangingState,
                PlayerState.Jumping => _jumpingState,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void CheckStateTransitions()
        {
            if (_currentStateValue != PlayerState.Idle && _currentStateHandler.IsDone())
            {
                Idle();
            }
        }

        public bool CanChangeState() => _currentStateValue == PlayerState.Idle;
        public void ChangingLane() => ChangeState(PlayerState.ChangingLane);
        public void Idle() => ChangeState(PlayerState.Idle);
        public void Jumping() => ChangeState(PlayerState.Jumping);
    }
}