using Core;
using UnityEngine;

namespace Movement
{
    public class FallingObject : GameBehavior
    {
        [SerializeField] private Transform obstaclePos;
        [SerializeField] private float heightDestroyObject = -50;
        
        private Rigidbody _rb;
        private bool _hasDropped;
        private Vector3 _targetPosition;
        private float _triggerDistance;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _targetPosition = Vector3.zero;
        }

        public void Initialize(float scrollSpeed)
        {
            float fallTime = Mathf.Sqrt(2 * obstaclePos.position.y / Mathf.Abs(Physics.gravity.y));
            _triggerDistance = scrollSpeed * fallTime;
        }

        protected override void GameplayUpdate()
        {
            if (transform.position.y <= heightDestroyObject)
            {
                Destroy(gameObject);
            }
            
            if (_hasDropped) return;
            float distanceToPlayer = transform.position.z - _targetPosition.z;

            if (distanceToPlayer <= _triggerDistance)
            {
                _rb.isKinematic = false;
                _hasDropped = true;
            }
        }
    }
}