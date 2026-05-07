using Player.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.State
{
    public class PlayerStateMachine
    {
        private IPlayerState _lane;
        private IPlayerState _jump;
        private IPlayerState _idle;
        private IPlayerState _slide;
        private IPlayerState _die;

        private IPlayerState _currentState;

        public PlayerStateMachine(PlayerController playerController, PlayerSettings playerSettings)
        {
            _idle = new IdleState();
            _die = new DieState(playerController, playerSettings.die);
            _jump = new JumpingState(playerController, playerSettings.jump);
            _lane = new LaneChangingState(playerController, playerSettings.changeLane);
            _slide = new SlideState(playerController, playerSettings.slide);
        }

        public void Start()
        {
            ChangeState(Idle());
        }

        public void ChangeState(IPlayerState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        
        public void UpdateState()
        {
            CheckStateTransitions();
            _currentState.UpdateState();
        }

        private void CheckStateTransitions()
        {
            if (_currentState is IdleState || !_currentState.IsDone()) return;
            switch (_currentState)
            {
                case JumpingState: 
                case SlideState:
                case LaneChangingState:
                    ChangeState(Idle());
                    break;
                case DieState:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
            }
        }

        public IPlayerState Idle() => _idle;
        public IPlayerState ChangingLane() => _lane;
        public IPlayerState Jumping() => _jump;
        public IPlayerState Sliding() => _slide;
        public IPlayerState Die() => _die;
        public bool CanJump() => _currentState is IdleState;
        public bool CanSlide() => _currentState is IdleState;
        public bool CanChangeLane() => _currentState is IdleState or LaneChangingState;
    }
}