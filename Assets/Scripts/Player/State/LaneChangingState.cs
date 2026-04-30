using UnityEngine;
using Utils;

namespace Player.State
{
    public class LaneChangingState : MonoBehaviour, IPlayerState
    {
        [Header("Settings")]
        [SerializeField] private float laneChangeDuration = 0.1f;
        [SerializeField] private float rotationSpeed = 1f;
        
        private float _elapsedTime;
        private float _startX;
        private float _targetX;

        public void Enter()
        {
            _elapsedTime = 0f;
            _startX = PlayerController.Instance.GetCurrentPosition().x;
            _targetX = PlayerController.Instance.GetCurrentLanePosition().x;
        }

        public void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            
            float t = _elapsedTime / laneChangeDuration;
            float moveFactor = EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
            float newX = Mathf.Lerp(_startX, _targetX, moveFactor);
            PlayerController.Instance.SetPositionX(newX);
        }

        public void Exit()
        {

        }

        public bool IsDone()
        {
            return _elapsedTime >= laneChangeDuration;
        }
    }
}
