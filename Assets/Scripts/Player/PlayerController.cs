using System;
using System.Collections.Generic;
using Player.Data;
using Player.State;
using UnityEngine;
using UnityEngine.InputSystem;
using World.GameElement;
using World.GameElement.Collectible;
using World.GameElement.Virus;

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
        [SerializeField] private Transform attachedPosition;
        [SerializeField] private GameObject meshGameObject;
        
        [Header("Lanes")]
        [SerializeField] private Transform[] laneAnchors;
        [SerializeField] private int initLaneIndex = 1;
        
        public static event Action<string> OnLetterCollected;
        public static event Action OnPlayerDied;
        
        private int _currentLaneIndex;
        private Transform _transform;
        private PlayerStateMachine _stateMachine;
        
        //Virus
        private VirusElement _currentVirusElement;
        private bool _isBlocked = false;

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
            if (_isBlocked) return;
            TryChangingLane(_currentLaneIndex - 1);
        }

        private void OnRightInput(InputAction.CallbackContext obj)
        {
            if (_isBlocked) return;
            TryChangingLane(_currentLaneIndex + 1);
        }

        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            if (_isBlocked) return;
            TryJumping();
        }
        
        private void OnSlideInput(InputAction.CallbackContext obj)
        {
            if (_isBlocked) return;
            TrySlide();
        }
        
        private void TryChangingLane(int newLaneIndex)
        {
            if (!_stateMachine.CanChangeLane()) return;
            newLaneIndex = Mathf.Clamp(newLaneIndex, 0, laneAnchors.Length - 1);
            if (newLaneIndex != _currentLaneIndex)
            {
                _currentLaneIndex = newLaneIndex;
                _stateMachine.ChangeState(_stateMachine.ChangingLane());
            }
        }

        private void TryJumping()
        {
            if (!_stateMachine.CanJump()) return;
            _stateMachine.ChangeState(_stateMachine.Jumping());
        }

        private void TrySlide()
        {
            if (!_stateMachine.CanSlide()) return;
            _stateMachine.ChangeState(_stateMachine.Sliding());
        }

        public void Update()
        {
            _stateMachine.UpdateState();
        }
        
        #region Virus

        public bool AttachVirus(VirusElement virus)
        {
            if (_currentVirusElement != null) return false;
            _currentVirusElement = virus;
            virus.ApplyEffect(this, attachedPosition);
            HUD.Instance.ShowVirusPanel(_currentVirusElement.GetLabel());
            return true;
        }

        public void DetachVirus()
        {
            if (_currentVirusElement == null) return;
            _currentVirusElement.RemoveEffect(this);
            HUD.Instance.HideVirusPanel();
            _currentVirusElement = null;
        }

        public void DisableMovement()
        {
            _isBlocked = true;
        }

        public void EnableMovement()
        {
            _isBlocked = false;
        }

        #endregion

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Element element))
            {
                element.OnPlayerCollision(this);
            }
        }
        
        public void CollectLetter(Letter letter)
        {
            OnLetterCollected?.Invoke(letter.Label);
        }

        public void Die()
        {
            OnPlayerDied?.Invoke();
            _stateMachine.ChangeState(_stateMachine.Die());
            
        }
        
        public Vector3 GetCurrentPosition() => _transform.position;
        public Vector3 GetCurrentScale() => _transform.localScale;
        public GameObject GetMeshObject() => meshGameObject;
        public Vector3 GetCurrentLanePosition() => laneAnchors[_currentLaneIndex].position;
        public bool IsJumpButtonPressed() => jumpInput.action.IsPressed();
        public bool IsSlideButtonPressed() => slideInput.action.IsPressed();

        #endregion
    }
}