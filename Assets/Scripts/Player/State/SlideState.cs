using Player.Data;
using UnityEngine;
using Utils;

namespace Player.State
{
    public class SlideState : IPlayerState
    {
        private readonly PlayerController _playerController;
        private readonly SlideSettings _slideSettings;
        
        private enum SlidePhase
        {
            Flatten,
            Slide,
            Recover
        }

        private SlidePhase _currentPhase;
        private float _elapsedTime;
        private float _recoverElapsedTime;
        private float _startScaleY;

        // Slide timing
        private float _slideStartTime;
        private float _shortSlideEndTime;

        public SlideState(PlayerController playerController, SlideSettings slideSettings)
        {
            _playerController = playerController;
            _slideSettings = slideSettings;
        }

        public void Enter()
        {
            _currentPhase = SlidePhase.Flatten;
            _elapsedTime = 0f;
            _recoverElapsedTime = 0f;
            _startScaleY = _playerController.GetCurrentScale().y;

            // Calculate phase timing
            _slideStartTime = _slideSettings.timeToFlatten;
            _shortSlideEndTime = _slideStartTime + _slideSettings.timeFlattenWait;
        }
        
        /// <summary>
        /// Jump is separated into 3 phases:
        /// 1. Ascending: Jump up to apex
        /// 2. Apex: Wait at peak (duration depends on button press)
        /// 3. Descending: Fall down
        /// </summary>
        public void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            float shrinkFactor = 0f;
            switch (_currentPhase)
            {
                case SlidePhase.Flatten:
                    shrinkFactor = UpdateFlatten();
                    break;
                case SlidePhase.Slide:
                    shrinkFactor = UpdateSlide();
                    break;
                case SlidePhase.Recover:
                    shrinkFactor = UpdateRecover();
                    break;
            }

            float newY = _slideSettings.slideShrinkHeight * shrinkFactor;
            _playerController.SetScaleY(newY);
        }
        
        private float UpdateFlatten()
        {
            if (_elapsedTime >= _slideStartTime)
            {
                _currentPhase = SlidePhase.Slide;
                return 1f;
            }

            float t = _elapsedTime / _slideSettings.timeFlattenWait;
            return EasingFunctions.EaseOutQuint(Mathf.Clamp01(t));
        }

        private float UpdateSlide()
        {
            bool shortApexExpired = _elapsedTime >= _shortSlideEndTime;
            bool slideButtonHeld = _playerController.IsSlideButtonPressed();

            if (shortApexExpired && !slideButtonHeld) // Waiting in slide
            {
                _currentPhase = SlidePhase.Recover;
            }
            return 1f;
        }

        private float UpdateRecover()
        {
            _recoverElapsedTime += Time.deltaTime;

            float t = _recoverElapsedTime / _slideSettings.timeToRecover;
            return EasingFunctions.EaseInQuint(Mathf.Clamp01(t));
        }

        public void Exit()
        {
            _playerController.SetScaleY(_startScaleY);
        }

        public bool IsDone()
        {
            return _currentPhase == SlidePhase.Recover
                   && _recoverElapsedTime >= _slideSettings.timeToFlatten;
        }
    }
}
