using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMove : MonoBehaviour
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
    
        private int _index;
        private Transform _transform;
        private PlayerInput _playerInput;

        private void Start()
        {
            _transform = transform;
            _index = initAnchorIndex;
            leftInput.action.started += HandleOnLeftInput;
            rightInput.action.started += HandleOnRightInput;
        }

        private void HandleOnLeftInput(InputAction.CallbackContext obj)
        {
            if (PlayerController.Instance.currentState == PlayerState.Ground && _index > 0)
                _index--;
        }
        
        private void HandleOnRightInput(InputAction.CallbackContext obj)
        {
            if (PlayerController.Instance.currentState == PlayerState.Ground && _index < lineAnchors.Length - 1)
                _index++;
        }

        void Update()
        {
            if (PlayerController.Instance.currentState == PlayerState.Ground)
            {
                Vector3 position = _transform.position;
                position.x = lineAnchors[_index].position.x;
                _transform.position = position;
            }
        }
    }
}
