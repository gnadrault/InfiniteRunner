using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerControllerLesson : MonoBehaviour
    {
        private enum PlayerState
        {
            Ground,
            Jumping
        }
        
        [SerializeField] private Transform[] lineAnchors;
        [SerializeField] private int initAnchorIndex;
        private int _index;
        
        [Header("References")]
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private PlayerJump playerJump;

        [Header("Input")]
        [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;
        
        private Transform _transform;
        
        private PlayerState _currentState = PlayerState.Ground;
        
        [SerializeField] private float speed = 10f;


        // State machine
        public delegate void OnUpdate();
        public OnUpdate onUpdate;

        public State currentState;
        
        [SerializeField] private float jumpHeight = 2f;
        private float _startTimer;
        private Vector3 _previousPosition;
        private float jumpDuration;


        public enum State
        {
            Idle,
            Jumping
        }
        
        private void Start()
        {
            // Setup
            _transform = transform;
            leftInput.action.started += HandleOnLeftInput; // delegate
            rightInput.action.started += HandleOnRightInput;
            jumpInput.action.started += HandleOnJumpInput;
            
            _index = initAnchorIndex;
            
            StartState(State.Idle);
        }
        
        private void HandleOnLeftInput(InputAction.CallbackContext obj)
        {
            Move(--_index);
        }
        
        private void HandleOnRightInput(InputAction.CallbackContext obj)
        {
            Move(++_index);
        }
        
        private void Move(int i)
        {
            _index = Mathf.Clamp(i, 0, lineAnchors.Length - 1);
            StartState(State.Jumping);
        }
        
        private void HandleOnJumpInput(InputAction.CallbackContext obj)
        {
            if (_currentState != PlayerState.Ground) return;
            _currentState = PlayerState.Jumping;
            playerJump.StartJump();
        }


        public void Update()
        {
            UpdateState();
        }

        #region State Machine

        public void StartState(State state)
        {
            ExitState(currentState);
            
            currentState = state;
            
            switch (state)
            {
                case State.Idle:
                    Idle_Start();
                    onUpdate = Idle_Update;
                    break;
                case State.Jumping:
                    Jumping_Start();
                    onUpdate = Jumping_Update;
                    break;
            }
        }

        public void UpdateState()
        {
            if (onUpdate != null)
                onUpdate();
        }

        public void ExitState(State state)
        {
            switch (state)
            {
                case State.Idle:
                    Idle_Exit();
                    break;
                case State.Jumping:
                    Jumping_Exit();
                    break;
            }
        }
        
        #region IDLE
        private void Idle_Start()
        {
            
        }
        
        private void Idle_Update()
        {
            Debug.Log("Idle");
        }
        
        private void Idle_Exit()
        {
            
        }
        #endregion
        
        #region JUMPING
        private void Jumping_Start()
        {
            _startTimer = 0f;
            _previousPosition = _transform.position;
        }
        
        private void Jumping_Update()
        {
            float t = _startTimer / jumpDuration;
            _transform.position = Vector3.Lerp(_previousPosition, lineAnchors[_index].position, t);
            _startTimer += Time.deltaTime;

            if (_startTimer >= jumpDuration)
                StartState(State.Idle);
        }
        
        private void Jumping_Exit()
        {
            
        }
        #endregion
        
        #endregion

        private void HandlePlayerState()
        {
            if (_currentState == PlayerState.Jumping && !playerJump.IsJumping())
            {
                _currentState = PlayerState.Ground;
            }
        }
    }
}
