using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Transform[] lineAnchors;
        [SerializeField]
        private int initAnchorIndex;
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private float rotationSpeed = 1f;
        
        public InputActionReference leftInput;
        public InputActionReference rightInput;
        public InputActionReference jumpInput;
    
        private int _index;
        private Transform _transform;
        private PlayerInput _playerInput;

        //public delegate void OnPlayerDeath();
        //public OnPlayerDeath onPlayerDeath;

        private void Start()
        {
            _transform = transform;
            _index = initAnchorIndex;
            leftInput.action.started += HandleOnLeftInput;
            rightInput.action.started += HandleOnRightInput;
            jumpInput.action.started += HandleOnJumpInput;
        }

        private void HandleOnLeftInput(InputAction.CallbackContext obj)
        {
            if (_index > 0)
                _index--;
        }
        
        private void HandleOnRightInput(InputAction.CallbackContext obj)
        {
            if (_index < lineAnchors.Length - 1)
                _index++;
        }
        
        private void HandleOnJumpInput(InputAction.CallbackContext obj)
        {
            Jump();
        }

        void Update()
        {
            _transform.position = Vector3.Lerp(_transform.position, lineAnchors[_index].position, Time.deltaTime * speed);
        }

        void Jump()
        {
            
        }
    }
}
