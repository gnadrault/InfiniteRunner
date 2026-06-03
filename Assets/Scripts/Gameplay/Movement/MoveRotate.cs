using Core;
using Shared;
using UnityEngine;

namespace Gameplay.Movement
{
    /// <summary>
    /// Script to rotate the game object
    /// </summary>
    public class MoveRotate : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private float rotateRange = 20f;
        [SerializeField] private FloatValue speed;
        
        private Quaternion _startRotation;
        private Transform _transform;
    
        void Start()
        {
            _transform = transform;
            _startRotation = _transform.rotation;
        }

        protected override void GameplayUpdate()
        {
            float angle = Mathf.Sin(Time.time * speed.Value) * rotateRange;
            _transform.rotation = _startRotation * Quaternion.Euler(0f, 0f, angle);
        }
    }
}
