using UnityEngine;
using Utils;

namespace Backup.StateMachine.State
{
    public class PlayerMovingState : PlayerBaseState
    {
        private readonly float _moveDuration;
        
        private float _elapsedTime;
        private float _startX;
        private float _targetX;

        public PlayerMovingState(PlayerStateMachine playerStateMachine, PlayerController playerController,
            float moveDuration) : base(playerStateMachine, playerController)
        {
            _moveDuration = moveDuration;
        }

        public override void Enter()
        {
            base.Enter();
            
            _elapsedTime = 0f;
            _startX = Controller.GetCurrentPosition().x;
            _targetX = Controller.GetTargetLinePosition().x;
        }

        public override void Update()
        {
            UpdateMovement();
            base.Update();
        }

        private void UpdateMovement()
        {
            _elapsedTime += Time.deltaTime;
            
            float t = _elapsedTime / _moveDuration;
            float moveFactor = EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
            float newX = Mathf.Lerp(_startX, _targetX, moveFactor);
            Controller.SetPositionX(newX);
        }

        protected override void CheckTransitions()
        {
            if (_elapsedTime >= _moveDuration)
            {
                StateMachine.ChangeState(Controller.IdleState);
            }
        }
    }
}