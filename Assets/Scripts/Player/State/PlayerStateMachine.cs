using System;
using UnityEngine;

namespace Player.State
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private LaneChangingState laneChangingState;
        [SerializeField] private JumpingState jumpingState;
        [SerializeField] private IdleState idleState;
        
        private PlayerState _currentStateValue;
        private IPlayerState _currentStateHandler;
        
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
        
        public void ChangeState(PlayerState newPlayerState)
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
                PlayerState.Idle => idleState,
                PlayerState.ChangingLane => laneChangingState,
                PlayerState.Jumping => jumpingState,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void CheckStateTransitions()
        {
            if (_currentStateHandler.IsDone() && _currentStateValue != PlayerState.Idle)
            {
                ChangeState(PlayerState.Idle);
            }
        }

        public PlayerState GetCurrentState()
        {
            return _currentStateValue;
        }
    }
}