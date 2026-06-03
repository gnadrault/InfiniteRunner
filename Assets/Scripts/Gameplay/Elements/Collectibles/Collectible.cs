using Shared;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    /// <summary>
    /// Abstract class for collectible types game objects
    /// </summary>
    public abstract class Collectible : Element
    {
        [Header("Settings")]
        [SerializeField] private FloatValue point;
        
        public int Point => (int)point.Value;

        private bool _magnetActivated;
        private float _magnetForce;
        private Transform _targetPosition;
        private Transform _transform;

        public void ActivateMagnet(Transform position, float magnetForce)
        {
            _transform = transform;
            _targetPosition = position;
            _magnetForce = magnetForce;
            _magnetActivated = true;
        }

        protected override void GameplayUpdate()
        {
            if (_magnetActivated)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    _targetPosition.position,
                    Time.deltaTime * _magnetForce);
            }
        }
    }
}