using Core;
using Shared;
using UnityEngine;

namespace Gameplay.Movement
{
    /// <summary>
    /// Script to move horizontally the game object
    /// </summary>
    public class MoveHorizontal : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private FloatValue speed;
        [SerializeField] private float leftLaneX = -4;
        [SerializeField] private float rightLaneX = 4;

        private Vector3 _direction;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _direction = Random.value < 0.5f ? Vector3.left : Vector3.right;
        }

        protected override void GameplayUpdate()
        {
            Vector3 position = _transform.position;
            position.x += _direction.x * speed.Value * Time.deltaTime;
            _transform.position = position;
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (_transform.position.x >= rightLaneX)
                _direction = Vector3.left;
            else if (_transform.position.x <= leftLaneX)
                _direction = Vector3.right;
        }
    }
}