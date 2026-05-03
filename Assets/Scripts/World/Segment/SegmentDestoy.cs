using UnityEngine;

namespace World.Segment
{
    public class SegmentDestoy : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            Data.Segment segment = other.GetComponentInParent<Data.Segment>();
            
            if (segment != null)
            {
                Destroy(segment.gameObject);
            }
        }
    }
}
