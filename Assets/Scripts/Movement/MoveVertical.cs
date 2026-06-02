using Core;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// Script to move vertically the game object
    /// </summary>
    public class MoveVertical : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private float speed = 1f;
        [SerializeField] private float height = 5f;

        private Vector3 _direction;
        private Vector3 _initPosition;

        private void Start()
        {
            _initPosition = transform.position;
        }

        protected override void GameplayUpdate()
        {
            Vector3 position = transform.position;
            position.y += _direction.y * speed * Time.deltaTime;
            transform.position = position;
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (transform.position.y >= height)
            {
                _direction = Vector3.down;
            }
            else if (transform.position.y <= _initPosition.y)
            {
                _direction = Vector3.up;
            }
        }
    }
}