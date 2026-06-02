using Player.Data;
using UnityEngine;
using Utils;

namespace Player.State
{
    /// <summary>
    /// Manage the player changing lane state
    /// </summary>
    public class LaneChangingState : IPlayerState
    {
        private readonly ChangeLaneSettings _laneSettings;
        private float _elapsedTime;
        private float _startX;
        private float _targetX;

        public LaneChangingState(ChangeLaneSettings changeLaneSettings)
        {
            _laneSettings = changeLaneSettings;
        }

        public void Enter()
        {
            _elapsedTime = 0f;
            _startX = PlayerController.Instance.GetCurrentPosition().x;
            _targetX = PlayerController.Instance.GetCurrentLanePosition().x;
        }

        public void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            
            float t = _elapsedTime / _laneSettings.laneChangeDuration;
            float moveFactor = TweenUtils.EaseOutQuint(Mathf.Clamp01(t));
            float newX = Mathf.Lerp(_startX, _targetX, moveFactor);
            PlayerController.Instance.SetPositionX(newX);
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
