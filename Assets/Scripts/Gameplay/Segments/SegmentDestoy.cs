using UnityEngine;

namespace Gameplay.Segments
{
    public class SegmentDestoy : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            Segments.Segment segment = other.GetComponentInParent<Segments.Segment>();
            
            if (segment != null)
            {
                Destroy(segment.gameObject);
            }
        }
    }
}
