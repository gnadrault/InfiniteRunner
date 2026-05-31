using System;
using Core;
using UnityEngine;

namespace Gameplay.Segments
{
    public class SegmentDestoy : GameBehavior
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
