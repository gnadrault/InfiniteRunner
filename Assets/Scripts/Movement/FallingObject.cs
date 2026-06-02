using Core;
using Gameplay;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// Script for falling game objects
    /// </summary>
    public class FallingObject : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private Transform obstaclePos;
        [SerializeField] private float heightDestroyObject = -50;
        [SerializeField] private float triggerOffset = 5f;
        
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

        /// <summary>
        /// Initialize the distance trigger to fall using the current player position, the current speed
        /// An trigger offset can be applied
        /// </summary>
        public void Start()
        {
            float fallTime = Mathf.Sqrt(2 * obstaclePos.position.y / Mathf.Abs(Physics.gravity.y));
            _triggerDistance = SegmentManager.Instance.Speed * fallTime + triggerOffset;
        }

        protected override void GameplayUpdate()
        {
            if (transform.position.y <= heightDestroyObject)
            {
                Destroy(gameObject);
            }
            
            if (_hasDropped) return;
            float distanceToPlayer = transform.position.z - _targetPosition.z;

            if (distanceToPlayer <= _triggerDistance) // Fall object
            {
                _rb.isKinematic = false;
                _hasDropped = true;
            }
        }
    }
}