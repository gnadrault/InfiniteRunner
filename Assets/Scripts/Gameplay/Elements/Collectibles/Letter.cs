using System;
using Player;
using TMPro;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    public class Letter: Collectible
    {
        [SerializeField] private TextMeshPro label;

        private bool _magnetActivated;
        private float _magnetForce;
        private Transform _targetPosition;
        
        public string Label => label.text;

        public void SetLabelText(string text)
        {
            label.text = text;
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

        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            player.CollectLetter(this);
            Destroy(gameObject);
        }
    }
}