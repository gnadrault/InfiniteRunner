using System;
using System.Collections;
using Audio;
using Core;
using Data;
using Gameplay.Elements.Collectibles;
using Gameplay.Elements.Enemies;
using Player.Data;
using Player.State;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerController : GameBehavior
    {
        public static PlayerController Instance;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int Glow = Animator.StringToHash("Player_Glow");

        private enum MoveIntent
        {
            Left,
            Right,
            Jump,
            Slide
        }

        [Header("Input")] [SerializeField] private InputActionReference leftInput;
        [SerializeField] private InputActionReference rightInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private InputActionReference slideInput;

        [Header("Settings")] [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private GameObject meshGameObject;

        [Header("Lanes")] [SerializeField] private Transform[] laneAnchors;
        [SerializeField] private int initLaneIndex = 1;

        [Header("Effects")] [SerializeField] private GameObject shieldEffect;
        [SerializeField] private GameObject magnetEffect;
        [SerializeField] private Animator animator;

        private Renderer[] _renderers;
        private int _currentLaneIndex;
        private Transform _transform;
        private PlayerStateMachine _stateMachine;
        private MaterialPropertyBlock _matPropertyBlock;

        //Virus
        private Virus currentVirus;
        private bool _isBlocked;

        //Bonus / Malus States
        private bool _shield;
        private bool _ghost;
        private bool _freeze;
        private bool _invert;
        private float _delayTime;
        private float _multiplier = 1f;

        // Freeze count
        private int _countLeft;
        private int _countRight;
        private int _countJump;

        private void OnLeftInput(InputAction.CallbackContext _) => HandleIntent(MoveIntent.Left);
        private void OnRightInput(InputAction.CallbackContext _) => HandleIntent(MoveIntent.Right);
        private void OnJumpInput(InputAction.CallbackContext _) => HandleIntent(MoveIntent.Jump);
        private void OnSlideInput(InputAction.CallbackContext _) => HandleIntent(MoveIntent.Slide);

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _transform = transform;
                _currentLaneIndex = initLaneIndex;
                _renderers = meshGameObject.GetComponentsInChildren<Renderer>();
                _matPropertyBlock = new MaterialPropertyBlock();
                _stateMachine = new PlayerStateMachine(playerSettings);
            }
            else
                Destroy(this);
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

        private void HandleIntent(MoveIntent intent)
        {
            if (_isBlocked) return;
            if (_freeze && !ShouldPassFreeze(intent)) return;
            if (_invert) intent = Invert(intent);
            StartCoroutine(ExecuteIntent(intent));
        }

        private IEnumerator ExecuteIntent(MoveIntent intent)
        {
            yield return new WaitForSeconds(_delayTime);
            Action move = Execute(intent);
            move();
        }

        private Action Execute(MoveIntent intent)
        {
            return intent switch
            {
                MoveIntent.Left => () => TryChangingLane(_currentLaneIndex - 1),
                MoveIntent.Right => () => TryChangingLane(_currentLaneIndex + 1),
                MoveIntent.Jump => TryJumping,
                MoveIntent.Slide => TrySlide,
                _ => throw new ArgumentOutOfRangeException(nameof(intent), intent, null)
            };
        }

        private bool ShouldPassFreeze(MoveIntent intent)
        {
            int count = intent switch
            {
                MoveIntent.Left => ++_countLeft,
                MoveIntent.Right => ++_countRight,
                MoveIntent.Jump => ++_countJump,
                _ => 2
            };

            if (count < 2) return false;
            ResetFreezeCount();
            return true;
        }

        private MoveIntent Invert(MoveIntent intent)
        {
            return intent switch
            {
                MoveIntent.Left => MoveIntent.Right,
                MoveIntent.Right => MoveIntent.Left,
                _ => intent
            };
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

        protected override void GameplayUpdate()
        {
            _stateMachine.UpdateState();
        }

        #region Virus

        public void AttachVirus(Virus virus)
        {
            currentVirus = virus;
            GameEvents.OnVirusAttached?.Invoke();
            currentVirus.ApplyVirusEffect();
            AudioManager.Instance.PlayLoop(SfxType.VirusAttach);
        }

        public void DetachVirus()
        {
            if (!currentVirus) return;
            currentVirus.RemoveVirusEffect();
            currentVirus = null;
            AudioManager.Instance.StopLoop(SfxType.VirusAttach);
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
            ResetFreezeCount();
            _freeze = true;
        }

        private void ResetFreezeCount()
        {
            _countLeft = 0;
            _countRight = 0;
            _countJump = 0;
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
            magnetEffect.SetActive(true);
            AudioManager.Instance.PlayLoop(SfxType.Magnet);
        }

        public void RemoveMagnet()
        {
            magnetEffect.SetActive(false);
            AudioManager.Instance.StopLoop(SfxType.Magnet);
        }

        public void ApplyDelay(float delay)
        {
            _delayTime = delay;
        }

        public void RemoveDelay()
        {
            _delayTime = 0f;
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

        public void CollectLetter(LetterLoot letterLoot)
        {
            GameEvents.OnLetterCollected?.Invoke(letterLoot.Label);
            GameEvents.OnAddScorePoints?.Invoke(letterLoot.Point * _multiplier);
            animator.Play(Glow, 0, 0f);
            if (_multiplier > 1)
                AudioManager.Instance.PlayOneShot(SfxType.BonusCollect);
            else
                AudioManager.Instance.PlayLetterSound(letterLoot.Label);
        }

        public void CollectLoot(float point)
        {
            GameEvents.OnAddScorePoints?.Invoke(point * _multiplier);
            animator.Play(Glow, 0, 0f);
            AudioManager.Instance.PlayOneShot(_multiplier > 1 ? SfxType.BonusCollect : SfxType.LetterCollect);
        }

        public void Die()
        {
            GameEvents.OnPlayerDied?.Invoke();
            _stateMachine.ChangeState(_stateMachine.Die());
            AudioManager.Instance.StopAll();
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

        #endregion
    }
}