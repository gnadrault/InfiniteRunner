using Player.Data;
using Player.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private InputActionReference slideInput;
        
        [Header("Settings")]
        [SerializeField] private PlayerSettings playerSettings;
        
        [Header("Lanes")]
        [SerializeField] private Transform[] laneAnchors;
        [SerializeField] private int initLaneIndex = 1;
        
        private int _currentLaneIndex;
        private Transform _transform;
        private PlayerStateMachine _stateMachine;

        private void Awake()
        {
            _transform = transform;
            _currentLaneIndex = initLaneIndex;
            _stateMachine = new PlayerStateMachine(this, playerSettings);
        }

        private void Start()
        {
            _transform.position = laneAnchors[_currentLaneIndex].position;
            _stateMachine.InitializeState();
        }
        
        private void OnEnable()
        {
            leftInput.action.started += OnLeftInput;
            rightInput.action.started += OnRightInput;
            jumpInput.action.started += OnJumpInput;
            slideInput.action.started += OnSlideInput;
        }

        private void OnDisable()
        {
            leftInput.action.started -= OnLeftInput;
            rightInput.action.started -= OnRightInput;
            jumpInput.action.started -= OnJumpInput;
            slideInput.action.started -= OnSlideInput;
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
        
        private void OnSlideInput(InputAction.CallbackContext obj)
        {
            TrySlide();
        }
        
        private void TryChangingLane(int newLaneIndex)
        {
            if (!_stateMachine.CanChangeState()) return;
            newLaneIndex = Mathf.Clamp(newLaneIndex, 0, laneAnchors.Length - 1);
            if (newLaneIndex != _currentLaneIndex)
            {
                _currentLaneIndex = newLaneIndex;
                _stateMachine.ChangeState(_stateMachine.ChangingLane());
            }
        }

        private void TryJumping()
        {
            if (!_stateMachine.CanChangeState()) return;
            _stateMachine.ChangeState(_stateMachine.Jumping());
        }

        private void TrySlide()
        {
            if (!_stateMachine.CanChangeState()) return;
            _stateMachine.ChangeState(_stateMachine.Sliding());
        }

        public void Update()
        {
            _stateMachine.UpdateState();
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

        public void SetScaleY(float y)
        {
            Vector3 scale = _transform.localScale;
            scale.y = y;
            _transform.localScale = scale;
        }
        
        public Vector3 GetCurrentPosition() => _transform.position;
        public Vector3 GetCurrentScale() => _transform.localScale;
        public Vector3 GetCurrentLanePosition() => laneAnchors[_currentLaneIndex].position;
        public bool IsJumpButtonPressed() => jumpInput.action.IsPressed();
        public bool IsSlideButtonPressed() => slideInput.action.IsPressed();

        #endregion
    }
}