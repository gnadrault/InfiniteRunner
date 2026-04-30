using Backup.StateMachine.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Backup.StateMachine
{
    public class PlayerController : MonoBehaviour
    {
        private Transform _transform;
        private PlayerStateMachine _stateMachine;
        
        [Header("Input")] 
        [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;

        [Header("Movement Settings")]
        [SerializeField] private Transform[] lineAnchors;
        [SerializeField] private int initAnchorIndex = 1;
        [SerializeField] private float moveDuration = 0.15f;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float timeToApex = 0.1f; // Time to reach the Apex
        [SerializeField] private float timeApexWait = 0.2f; // Time waiting on Apex (1 input)
        [SerializeField] private float timeApexExtraWait = 0.2f; // Time waiting on Apex (keep pressed)
        [SerializeField] private float timeDescent = 0.2f; // Time to descent from Apex
        
        // State machine
        public PlayerBaseState IdleState { get; private set; }
        public PlayerBaseState MovingState { get; private set; }
        public PlayerBaseState JumpingState { get; private set; }

        private int _currentAnchorIndex;
        private int _targetAnchorIndex;
        
        // Input flags
        public bool IsMoveRequested { get; private set; }
        public bool IsJumpRequested { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _currentAnchorIndex = initAnchorIndex;
            
            _stateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(_stateMachine, this);
            MovingState = new PlayerMovingState(_stateMachine, this, moveDuration);
            JumpingState = new PlayerJumpingState(
                _stateMachine, 
                this, 
                timeToApex,
                timeApexWait, 
                timeApexExtraWait, 
                timeDescent, 
                jumpHeight
            );
        }

        private void Start()
        {
            _stateMachine.Initialize(IdleState);
        }

        private void OnEnable()
        {
            leftInput.action.started += OnLeftInput;
            rightInput.action.started += OnRightInput;
            jumpInput.action.started += OnJumpInput;
        }

        private void OnDisable()
        {
            leftInput.action.started -= OnLeftInput;
            rightInput.action.started -= OnRightInput;
            jumpInput.action.started -= OnJumpInput;
        }
        
        private void Update()
        {
            _stateMachine.UpdateState();
        }

        private void OnLeftInput(InputAction.CallbackContext obj)
        {
            RequestMove(_currentAnchorIndex - 1);
        }

        private void OnRightInput(InputAction.CallbackContext obj)
        {
            RequestMove(_currentAnchorIndex + 1);
        }

        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            RequestJump();
        }
        
        private void RequestMove(int i)
        {
            _targetAnchorIndex = Mathf.Clamp(i, 0, lineAnchors.Length - 1);
            IsMoveRequested = true;
        }
        
        private void RequestJump()
        {
            IsJumpRequested = true;
        }
        
        /// <summary>
        /// Consume and reset input flags.
        /// </summary>
        public void ConsumeInputFlags()
        {
            IsMoveRequested = false;
            IsJumpRequested = false;
        }

        /// <summary>
        /// Set horizontal position (X axis)
        /// </summary>
        public void SetPositionX(float x)
        {
            Vector3 position = _transform.position;
            position.x = x;
            _transform.position = position;
        }
        
        /// <summary>
        /// Set vertical position (Y axis)
        /// </summary>
        public void SetPositionY(float y)
        {
            Vector3 position = _transform.position;
            position.y = y;
            _transform.position = position;
        }
        
        /// <summary>
        /// Update current anchor index after successful move
        /// </summary>
        public void SetCurrentAnchorIndex(int index)
        {
            _currentAnchorIndex = index;
        }
        
        public Vector3 GetCurrentPosition()
        {
            return _transform.position;
        }
        
        public Vector3 GetTargetLinePosition()
        {
            return lineAnchors[_targetAnchorIndex].position;
        }
        
        public Vector3 GetCurrentLinePosition()
        {
            return lineAnchors[_currentAnchorIndex].position;
        }
        
        public bool IsJumpButtonPressed()
        {
            return jumpInput.action.IsPressed();
        }
    }
}