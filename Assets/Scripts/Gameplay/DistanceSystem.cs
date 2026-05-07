using TMPro;
using UnityEngine;
using World.Segment;

namespace Gameplay
{
    public class DistanceSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI distanceLabel;
        
        private SegmentManager _segmentManager;
        
        private float distance = 0f;

        private void Awake()
        {
            _segmentManager = GetComponent<SegmentManager>();
        }

        private void Update()
        {
            distance += (Time.deltaTime * _segmentManager.ScrollSpeed);
            distanceLabel.text = (int)distance + "m";
        }
    }
}
