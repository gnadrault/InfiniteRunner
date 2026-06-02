using Core;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    /// <summary>
    /// Manage the distance of the gameplay
    /// </summary>
    public class DistanceTracker : GameBehavior
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI distanceLabel;
        
        [Header("Settings")]
        [SerializeField] private float distanceScale = 1f;
        
        private float _distance;
        private float _speed;

        private void OnEnable()
        {
            GameEvents.OnSpeedChanged += HandleSpeedChanged;
        }
        
        private void OnDisable()
        {
            GameEvents.OnSpeedChanged -= HandleSpeedChanged;
        }
        
        private void HandleSpeedChanged(float speed)
        {
            _speed = speed;
        }

        protected override void GameplayUpdate()
        {
            float oldDistance = _distance;
            _distance += (Time.deltaTime * _speed * distanceScale);
            if ((int)_distance > (int)oldDistance)
            {
                GameEvents.OnNewMeter?.Invoke(_distance); // Notify on new distinct meter
            }
            distanceLabel.text = $"{((int)_distance)} m"; // Update the distance display text
        }
    }
}
