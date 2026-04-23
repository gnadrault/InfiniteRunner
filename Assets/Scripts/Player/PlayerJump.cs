using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float timeToApex = 0.1f; // Time to reach the Apex
        [SerializeField] private float timeApexWait = 0.2f; // Time waiting on Apex (1 input)
        [SerializeField] private float timeApexExtraWait = 0.2f; // Time waiting on Apex (keep pressed)
        [SerializeField] private float timeDescent = 0.2f; // Time to descent from Apex 
        
        private float _elapsedTimeJump;
        private float _elapsedTimeDown;
        private float _tUpEnd;
        private float _tShortApexEnd;
        private float _tLongApexEnd;
        private bool _isJumping;

        public void StartJump()
        {
            _elapsedTimeDown = 0;
            _elapsedTimeJump = 0;
            _tUpEnd = timeToApex;
            _tShortApexEnd = _tUpEnd + timeApexWait;
            _tLongApexEnd = _tShortApexEnd + timeApexExtraWait;
            _isJumping = true;
        }

        public bool IsJumping()
        {
            return _isJumping;
        }
        
        /// <summary>
        /// Jumping is separate in 3 phases
        /// - Jump up to apex
        /// - Waiting in apex (depends on keypressed state)
        /// - Falling
        /// </summary>
        public float HandlePlayerJump(bool isPressed)
        {
            float heightFactor = 0;
            if (_isJumping)
            {
                _elapsedTimeJump += Time.deltaTime;
                if (_elapsedTimeJump < _tUpEnd) // Jump up
                {
                    float t = _elapsedTimeJump / timeToApex;
                    heightFactor = EvaluateUp(t);
                }
                else if ((_elapsedTimeJump < _tShortApexEnd) || 
                         (_elapsedTimeJump < _tLongApexEnd && isPressed)) // Waiting in apex
                {
                    float t = (_elapsedTimeJump - _tUpEnd) / timeApexWait;
                    heightFactor = EvaluateApex(t);
                }
                else // Down
                {
                    _elapsedTimeDown += Time.deltaTime;
                    float t = _elapsedTimeDown / timeDescent;
                    heightFactor = EvaluateDown(t);
                    _isJumping = t < 1; // End jump
                }
            }
            return heightFactor * jumpHeight;
        }
        
        private float EvaluateUp(float t)
        {
            t = Mathf.Clamp01(t);
            return EasingFunctions.EaseOutQuint(t);
        }
        
        private float EvaluateApex(float t)
        {
            return 1f;
        }
        
        private float EvaluateDown(float t)
        {
            t = Mathf.Clamp01(t);
            return EasingFunctions.EaseInQuint(t);
        }
    }
}
