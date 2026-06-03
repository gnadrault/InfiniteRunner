using Core;
using UnityEngine;

namespace Gameplay.Segments
{
    /// <summary>
    /// Script to destroy segments on collision with the current game object
    /// </summary>
    public class SegmentDestroy : GameBehavior
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
