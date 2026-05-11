using UnityEngine;

namespace World.GameElement.Obstacle
{
    [System.Serializable]
    public class ObstacleFalling : ObstacleElement
    {
        private Rigidbody _rb;
        private float _height;
        private bool _hasDropped = false;
        
        private Transform _playerPosition;
        private float _scrollSpeed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = false;
            _height = transform.position.y;
        }

        private void Initalize(Transform playerPosition, float scrollSpeed)
        {
            _playerPosition = playerPosition;
            _scrollSpeed = scrollSpeed;
        }

        private void Update()
        {
            if (_hasDropped) return;
            
            float fallTime = Mathf.Sqrt(2 * _height / Mathf.Abs(Physics.gravity.y));
            float triggerDistance = _scrollSpeed * fallTime;
            float distanceToPlayer = transform.position.z - _playerPosition.position.z;

            if (distanceToPlayer <= triggerDistance)
            {
                _rb.isKinematic = false;
                _hasDropped = true;
            }
        }
    }
}
