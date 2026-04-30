using UnityEngine;

namespace World
{
    public class SegmentDestoy : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Segment chunk))
            {
                Destroy(chunk.gameObject);
            }
        }
    }
}
