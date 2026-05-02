using UnityEngine;

namespace World
{
    public class SegmentDestoy : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            Segment segment = other.GetComponentInParent<Segment>();
            
            if (segment != null)
            {
                Destroy(segment.gameObject);
            }
        }
    }
}
