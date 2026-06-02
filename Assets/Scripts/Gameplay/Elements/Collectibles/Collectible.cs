using Data;
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

        private bool _magnetActivated;
        private float _magnetForce;
        private Transform _targetPosition;

        public int Point => (int)point.Value;

        public void ActivateMagnet(Transform position, float magnetForce)
        {
            _targetPosition = position;
            _magnetForce = magnetForce;
            _magnetActivated = true;
        }

        protected override void GameplayUpdate()
        {
            if (_magnetActivated)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _targetPosition.position,
                    Time.deltaTime * _magnetForce);
            }
        }
    }
}