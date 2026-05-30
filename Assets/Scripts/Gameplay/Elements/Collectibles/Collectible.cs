using System;
using Data;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    public abstract class Collectible : Element
    {
        [SerializeField] private ValuePoint point;

        private bool _magnetActivated;
        private float _magnetForce;
        private Transform _targetPosition;

        public int Point => point.Value;

        private void Awake()
        {
            print("Collectible Awake : " + point.Value);
        }

        public void ActivateMagnet(Transform position, float magnetForce)
        {
            _targetPosition = position;
            _magnetForce = magnetForce;
            _magnetActivated = true;
        }

        private void Update()
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