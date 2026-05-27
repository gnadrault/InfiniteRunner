using Data;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class DistanceTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI distanceLabel;
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

        private void Update()
        {
            float oldDistance = _distance;
            _distance += (Time.deltaTime * _speed * distanceScale);
            if ((int)_distance > (int)oldDistance)
            {
                GameEvents.OnNewMeter?.Invoke(_distance);
            }
            distanceLabel.text = $"{((int)_distance)} m";
        }
    }
}
