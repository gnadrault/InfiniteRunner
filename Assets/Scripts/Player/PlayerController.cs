using System;
using Player.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        [Header("Input")] 
        [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;
        
        [Header("References")]
        [SerializeField] private LaneChangingState laneChangingState;
        [SerializeField] private JumpingState jumpingState;
        [SerializeField] private IdleState idleState;
        
        [Header("Lane Anchors")]
        [SerializeField] private Transform[] laneAnchors;
        [SerializeField] private int initLaneIndex = 1;

        public enum PlayerState
        {
            Idle,
            ChangingLane,
            Jumping
        }
        
        private Transform _transform;
        private int _currentLaneIndex;
        private PlayerState _currentStateValue;
        private IPlayerState _currentStateHandler;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _transform = transform;
            _currentLaneIndex = initLaneIndex;
            _transform.position = laneAnchors[_currentLaneIndex].position;
            
            InitializeState();
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

        private void OnLeftInput(InputAction.CallbackContext obj)
        {
            TryChangingLane(_currentLaneIndex - 1);
        }

        private void OnRightInput(InputAction.CallbackContext obj)
        {
            TryChangingLane(_currentLaneIndex + 1);
        }

        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            TryJumping();
        }
        
        private void TryChangingLane(int newLaneIndex)
        {
            if (_currentStateValue != PlayerState.Idle) return;
            newLaneIndex = Mathf.Clamp(newLaneIndex, 0, laneAnchors.Length - 1);
            if (newLaneIndex != _currentLaneIndex)
            {
                _currentLaneIndex = newLaneIndex;
                ChangeState(PlayerState.ChangingLane);
            }
        }

        private void TryJumping()
        {
            if (_currentStateValue != PlayerState.Idle) return;
            ChangeState(PlayerState.Jumping);
        }

        public void Update()
        {
            UpdateState();
        }

        #region Getters/Setters

        public void SetPositionX(float x)
        {
            Vector3 position = _transform.position;
            position.x = x;
            _transform.position = position;
        }
        
        public void SetPositionY(float y)
        {
            Vector3 position = _transform.position;
            position.y = y;
            _transform.position = position;
        }
        
        public Vector3 GetCurrentPosition()
        {
            return _transform.position;
        }
        
        public Vector3 GetCurrentLanePosition()
        {
            return laneAnchors[_currentLaneIndex].position;
        }
        
        public bool IsJumpButtonPressed()
        {
            return jumpInput.action.IsPressed();
        }

        #endregion


        #region State Machine
        
        public void InitializeState()
        {
            SetState(PlayerState.Idle);
            _currentStateHandler.Enter();
        }
        
        public void UpdateState()
        {
            CheckStateTransitions();
            _currentStateHandler.UpdateState();
        }
        
        public void ChangeState(PlayerState newPlayerState)
        {
            _currentStateHandler.Exit();
            SetState(newPlayerState);
            _currentStateHandler.Enter();
        }

        private void SetState(PlayerState newPlayerState)
        {
            _currentStateValue = newPlayerState;
            _currentStateHandler = _currentStateValue switch
            {
                PlayerState.Idle => idleState,
                PlayerState.ChangingLane => laneChangingState,
                PlayerState.Jumping => jumpingState,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void CheckStateTransitions()
        {
            if (_currentStateHandler.IsDone() && _currentStateValue != PlayerState.Idle)
            {
                ChangeState(PlayerState.Idle);
            }
        }
        
        #endregion
    }
}