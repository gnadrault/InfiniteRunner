using TMPro;
using UnityEngine;
using Utils;
using World.Segment;

namespace Gameplay
{
    public class DistanceSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI distanceLabel;
        [SerializeField] private float distanceScale = 0.2f;
        
        private SegmentManager _segmentManager;
        private float distance;

        private void Awake()
        {
            _segmentManager = GetComponent<SegmentManager>();
        }

        private void Update()
        {
            float oldDistance = distance;
            distance += (Time.deltaTime * _segmentManager.ScrollSpeed * distanceScale);
            if ((int)distance > (int)oldDistance)
            {
                GameEvents.OnNewMeter?.Invoke(distance);
            }
            distanceLabel.text = ((int)distance).ToString();
        }
    }
}
