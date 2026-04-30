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
        [SerializeField] private PlayerStateMachine stateMachine;
        
        [Header("Lane Anchors")]
        [SerializeField] private Transform[] laneAnchors;
        [SerializeField] private int initLaneIndex = 1;
        
        private Transform _transform;
        private int _currentLaneIndex;


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
            
            stateMachine.InitializeState();
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
            if (stateMachine.GetCurrentState() != PlayerState.Idle) return;
            newLaneIndex = Mathf.Clamp(newLaneIndex, 0, laneAnchors.Length - 1);
            if (newLaneIndex != _currentLaneIndex)
            {
                _currentLaneIndex = newLaneIndex;
                stateMachine.ChangeState(PlayerState.ChangingLane);
            }
        }

        private void TryJumping()
        {
            if (stateMachine.GetCurrentState() != PlayerState.Idle) return;
            stateMachine.ChangeState(PlayerState.Jumping);
        }

        public void Update()
        {
            stateMachine.UpdateState();
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
    }
}