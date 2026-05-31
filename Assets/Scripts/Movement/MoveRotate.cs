using Core;
using UnityEngine;

namespace Movement
{
    public class MoveRotate : GameBehavior
    {
        [SerializeField] private float rotateRange = 20f;
        [SerializeField] private float speed = 2f;
        
        private Quaternion _startRotation;
    
        void Start()
        {
            _startRotation = transform.rotation;
        }

        protected override void GameplayUpdate()
        {
            float angle = Mathf.Sin(Time.time * speed) * rotateRange;
            transform.rotation = _startRotation * Quaternion.Euler(0f, 0f, angle);
        }
    }
}
