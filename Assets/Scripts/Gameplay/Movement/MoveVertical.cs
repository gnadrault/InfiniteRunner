using Core;
using Shared;
using UnityEngine;

namespace Gameplay.Movement
{
    /// <summary>
    /// Script to move vertically the game object
    /// </summary>
    public class MoveVertical : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private FloatValue speed;
        [SerializeField] private float height = 5f;

        private Vector3 _direction;
        private Vector3 _initPosition;
        private Transform _transform;
		
        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _initPosition = _transform.position;
        }

        protected override void GameplayUpdate()
        {
            Vector3 position = _transform.position;
            position.y += _direction.y * speed.Value * Time.deltaTime;
            _transform.position = position;
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (_transform.position.y >= height)
            {
                _direction = Vector3.down;
            }
            else if (_transform.position.y <= _initPosition.y)
            {
                _direction = Vector3.up;
            }
        }
    }
}