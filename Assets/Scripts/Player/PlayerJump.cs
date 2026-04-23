using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float timeToApex = 0.1f; // Time to reach the Apex
        [SerializeField] private float timeApexWait = 0.2f; // Time waiting on Apex (1 input)
        [SerializeField] private float timeApexExtraWait = 0.2f; // Time waiting on Apex (keep pressed)
        [SerializeField] private float timeDescent = 0.2f; // Time to descent from Apex 
        
        public InputActionReference jumpInput;
        
        private Transform _transform;
        private PlayerInput _playerInput;
        private float _elapsedTimeJump;
        private float _elapsedTimeDown;
        private float _startHeight;

        private float _tUpEnd;
        private float _tShortApexEnd;
        private float _tLongApexEnd;
        
        private void Start()
        {
            _transform = transform;
            _startHeight = _transform.position.y;
            
            _tUpEnd = timeToApex;
            _tShortApexEnd = _tUpEnd + timeApexWait;
            _tLongApexEnd = _tShortApexEnd + timeApexExtraWait;
            jumpInput.action.started += HandleOnJumpInput;
        }
        
        private void HandleOnJumpInput(InputAction.CallbackContext obj)
        {
            if (PlayerController.Instance.currentState == PlayerState.Ground)
            {
                _elapsedTimeDown = 0;
                _elapsedTimeJump = 0;
                PlayerController.Instance.currentState = PlayerState.Jumping;
            }
        }
        
        /// <summary>
        /// Jumping is separate in 3 phases
        /// - Jump up to apex
        /// - Waiting in apex (depends on keypressed state)
        /// - Falling
        /// </summary>
        void Update()
        {
            if (PlayerController.Instance.currentState == PlayerState.Jumping)
            {
                Vector3 position = _transform.position;
                float heightFactor;

                _elapsedTimeJump += Time.deltaTime;
                if (_elapsedTimeJump < _tUpEnd) // Jump up
                {
                    float t = _elapsedTimeJump / timeToApex;
                    heightFactor = EvaluateUp(t);
                }
                else if ((_elapsedTimeJump < _tShortApexEnd) || 
                         (_elapsedTimeJump < _tLongApexEnd && jumpInput.action.IsPressed())) // Waiting in apex
                {
                    float t = (_elapsedTimeJump - _tUpEnd) / timeApexWait;
                    heightFactor = EvaluateApex(t);
                }
                else // Down
                {
                    _elapsedTimeDown += Time.deltaTime;
                    float t = _elapsedTimeDown / timeDescent;
                    heightFactor = EvaluateDown(t);
                    if (t >= 1) // End jump
                    {
                        PlayerController.Instance.currentState = PlayerState.Ground;
                    }
                }
                position.y = _startHeight + heightFactor * jumpHeight;
                _transform.position = position;
            }
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
