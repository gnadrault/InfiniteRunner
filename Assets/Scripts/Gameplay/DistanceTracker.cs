using Data;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class DistanceTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI distanceLabel;
        [SerializeField] private float distanceScale = 0.2f;
        
        private float _distance;
        private float _speed;

        private void OnEnable()
        {
            GameEvents.OnNewPhase += HandleNewPhase;
        }
        
        private void OnDisable()
        {
            GameEvents.OnNewPhase -= HandleNewPhase;
        }

        private void HandleNewPhase(PhaseData phase)
        {
            _speed = phase.speed;
        }

        private void Update()
        {
            float oldDistance = _distance;
            _distance += (Time.deltaTime * _speed * distanceScale);
            if ((int)_distance > (int)oldDistance)
            {
                GameEvents.OnNewMeter?.Invoke(_distance);
            }
            distanceLabel.text = ((int)_distance).ToString();
        }
    }
}
