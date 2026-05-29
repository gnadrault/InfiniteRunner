using Player.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Player.State
{
    public class PlayerStateMachine
    {
        private readonly IPlayerState _lane;
        private readonly IPlayerState _jump;
        private readonly IPlayerState _idle;
        private readonly IPlayerState _slide;
        private readonly IPlayerState _die;

        private IPlayerState _currentState;
        private readonly ParticleSystem _trailsParticles;
        private bool _hasAlreadyEndGame;

        public PlayerStateMachine(PlayerController playerController, PlayerSettings playerSettings)
        {
            _trailsParticles = playerSettings.idle.trailsParticles;
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
            ApplyTrailsParticles();
            _currentState.UpdateState();
        }

        private void ApplyTrailsParticles()
        {
            if (_currentState is IdleState or SlideState)
            {
                if (!_trailsParticles.isPlaying)
                    _trailsParticles.Play();
            }
            else
            {
                if (_trailsParticles.isPlaying)
                    _trailsParticles.Stop();
            }
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
                    if (!_hasAlreadyEndGame)
                    {
                        GameEvents.OnEndGame?.Invoke();
                        _hasAlreadyEndGame = true;
                    }
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