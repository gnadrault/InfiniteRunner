using Player.Data;

namespace Player.State
{
    public class PlayerStateMachine
    {
        private IPlayerState _lane;
        private IPlayerState _jump;
        private IPlayerState _idle;
        
        private IPlayerState _currentState;

        public PlayerStateMachine(PlayerController playerController, PlayerSettings playerSettings)
        {
            _idle = new IdleState();
            _jump = new JumpingState(playerController, playerSettings.jump);
            _lane = new LaneChangingState(playerController, playerSettings.changeLane);
        }
        
        public void InitializeState()
        {
            _currentState = Idle();
            _currentState.Enter();
        }
        
        public void UpdateState()
        {
            CheckStateTransitions();
            _currentState.UpdateState();
        }
        
        public void ChangeState(IPlayerState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void CheckStateTransitions()
        {
            if (_currentState != Idle() && _currentState.IsDone())
            {
                ChangeState(Idle());
            }
        }

        public IPlayerState Idle() => _idle;
        public IPlayerState ChangingLane() => _lane;
        public IPlayerState Jumping() => _jump;
        public bool CanChangeState() => _currentState == Idle();
    }
}