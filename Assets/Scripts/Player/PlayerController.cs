using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Elements.Collectibles;
using Gameplay.Elements.Enemies;
using Player.Data;
using Player.State;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

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
        
        [Header("Effects")]
        [SerializeField] private GameObject shieldEffect;
        [SerializeField] private GameObject magnetEffect;
        
        private int _currentLaneIndex;
        private Transform _transform;
        private PlayerStateMachine _stateMachine;
        private MaterialPropertyBlock _matPropertyBlock;
        
        private Renderer[] _renderers;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        
        //Virus
        private Virus currentVirus;
        private bool _isBlocked;
        
        //Bonus / Malus States
        private bool _shield;
        private bool _ghost;
        private bool _freeze;
        private bool _invert;
        private float _multiplier;
        private bool _magnet;

        private int _countLeft;
        private int _countRight;
        private int _countJump;

        private void Awake()
        {
            _transform = transform;
            _currentLaneIndex = initLaneIndex;
            _renderers = meshGameObject.GetComponentsInChildren<Renderer>();
            _matPropertyBlock =  new MaterialPropertyBlock();
            _stateMachine = new PlayerStateMachine(this, playerSettings);
        }

        private void Start()
        {
            _transform.position = laneAnchors[_currentLaneIndex].position;
            _stateMachine.Start();
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
            if (_freeze)
            {
                _countLeft++;
                if (_countLeft < 2) return;
                _countLeft = 0;
            } else if (_invert)
            {
                OnRightInput(obj);
            }
            TryChangingLane(_currentLaneIndex - 1);
        }

        private void OnRightInput(InputAction.CallbackContext obj)
        {
            if (_isBlocked) return;
            if (_freeze)
            {
                _countRight++;
                if (_countRight < 2) return;
                _countRight = 0;
            } else if (_invert)
            {
                OnLeftInput(obj);
            }
            TryChangingLane(_currentLaneIndex + 1);
        }

        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            if (_isBlocked) return;
            if (_freeze)
            {
                _countJump++;
                if (_countJump < 2) return;
                _countJump = 0;
            }
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

        public void AttachVirus(Virus virus)
        {
            currentVirus = virus;
            GameEvents.OnVirusAttached?.Invoke();
            currentVirus.ApplyEffect(this, attachedPosition);
        }

        private IEnumerator WaitAndApplyVirusEffect(Virus virus)
        {
            currentVirus = virus;
            GameEvents.OnVirusAttached?.Invoke();
            yield return new WaitForSeconds(2);
            currentVirus.ApplyEffect(this, attachedPosition);
        }
        
        public void DetachVirus()
        {
            if (!currentVirus) return;
            currentVirus.RemoveEffect(this);
            currentVirus = null;
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

        #region Bonus / Malus

        public void ApplyShield()
        {
            _shield = true;
            shieldEffect.SetActive(_shield);
        }

        public void RemoveShield()
        {
            _shield = false;
            shieldEffect.SetActive(_shield);
        }

        public void ApplyGhost()
        {
            _ghost = true;
            Colors.SetTransparency(_renderers, _matPropertyBlock, BaseColor, 0.1f);
        }

        public void RemoveGhost()
        {
            _ghost = false;
            Colors.SetTransparency(_renderers, _matPropertyBlock, BaseColor, 1f);
        }
        
        public void ApplyFreeze()
        {
            _countLeft = 0;
            _countRight = 0;
            _countJump = 0;
            _freeze = true;
        }

        public void RemoveFreeze()
        {
            _freeze = false;
        }
        
        public void ApplyInvert()
        {
            _invert = true;
        }

        public void RemoveInvert()
        {
            _invert = false;
        }
        
        public void ApplyMultiplier(float multiplierFactor)
        {
            _multiplier = multiplierFactor;
        }

        public void RemoveMultiplier()
        {
            _multiplier = 1;
        }
        
        public void ApplyMagnet()
        {
            _magnet = true;
            magnetEffect.SetActive(_magnet);
        }

        public void RemoveMagnet()
        {
            _magnet = false;
            magnetEffect.SetActive(_magnet);
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
        
        public void CollectLetter(Letter letter)
        {
            GameEvents.OnLetterCollected?.Invoke(letter.Label);
            CollectLoot(letter.Point);
        }

        public void CollectLoot(float point)
        {
            GameEvents.OnAddScorePoints?.Invoke(point * _multiplier);
        }

        public void Die()
        {
            GameEvents.OnPlayerDied?.Invoke();
            _stateMachine.ChangeState(_stateMachine.Die());
        }
        
        public Vector3 GetCurrentPosition() => _transform.position;
        public Vector3 GetCurrentScale() => _transform.localScale;
        public GameObject GetMeshObject() => meshGameObject;
        public Vector3 GetCurrentLanePosition() => laneAnchors[_currentLaneIndex].position;
        public bool IsJumpButtonPressed() => jumpInput.action.IsPressed();
        public bool IsSlideButtonPressed() => slideInput.action.IsPressed();
        public bool IsPlayerInfected() => currentVirus;
        public bool HasShield() => _shield;
        public bool HasGhost() => _ghost;
        public bool HasMagnet() => _magnet;

        #endregion
    }
}