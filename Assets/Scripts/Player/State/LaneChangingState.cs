using Player.Data;
using UnityEngine;
using Utils;

namespace Player.State
{
    public class LaneChangingState : IPlayerState
    {
        private readonly PlayerController _playerController;
        private readonly ChangeLaneSettings _laneSettings;
        private float _elapsedTime;
        private float _startX;
        private float _targetX;

        public LaneChangingState(PlayerController playerController, ChangeLaneSettings changeLaneSettings)
        {
            _playerController = playerController;
            _laneSettings = changeLaneSettings;
        }

        public void Enter()
        {
            _elapsedTime = 0f;
            _startX = _playerController.GetCurrentPosition().x;
            _targetX = _playerController.GetCurrentLanePosition().x;
        }

        public void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            
            float t = _elapsedTime / _laneSettings.laneChangeDuration;
            float moveFactor = EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
            float newX = Mathf.Lerp(_startX, _targetX, moveFactor);
            _playerController.SetPositionX(newX);
        }

        public void Exit()
        {
            
        }

        public bool IsDone()
        {
            return _elapsedTime >= _laneSettings.laneChangeDuration;
        }
    }
}
