using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerController : MonoBehaviour
    {
        private enum PlayerState
        {
            Ground,
            Jumping
        }
        
        [Header("References")]
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private PlayerJump playerJump;

        [Header("Input")]
        [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;
        
        private Transform _transform;
        private float _startHeight;
        
        private PlayerState _currentState = PlayerState.Ground;
        
        private void Start()
        {
            _transform = transform;
            _startHeight = _transform.position.y;
            leftInput.action.started += HandleOnLeftInput;
            rightInput.action.started += HandleOnRightInput;
            jumpInput.action.started += HandleOnJumpInput;
        }
        
        private void HandleOnLeftInput(InputAction.CallbackContext obj)
        {
            if (_currentState == PlayerState.Ground)
                playerMove.MoveLeft();
        }
        
        private void HandleOnRightInput(InputAction.CallbackContext obj)
        {
            if (_currentState == PlayerState.Ground)
                playerMove.MoveRight();
        }
        
        private void HandleOnJumpInput(InputAction.CallbackContext obj)
        {
            if (_currentState != PlayerState.Ground) return;
            _currentState = PlayerState.Jumping;
            playerJump.StartJump();
        }

        public void Update()
        {
            HandlePlayerState();
            
            Vector3 position = _transform.position;
            if (_currentState == PlayerState.Jumping)
            {
                position.y = _startHeight + playerJump.HandlePlayerJump(jumpInput.action.IsPressed());
            } else if (_currentState == PlayerState.Ground)
            {
                position.x = playerMove.HandlePlayerMove();
            }
            _transform.position = position;
        }

        private void HandlePlayerState()
        {
            if (_currentState == PlayerState.Jumping && !playerJump.IsJumping())
            {
                _currentState = PlayerState.Ground;
            }
        }
    }
}
