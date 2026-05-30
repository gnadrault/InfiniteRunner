using System;
using UnityEngine;

namespace Gameplay.Segments
{
    public class SegmentDestoy : MonoBehaviour
    {
        private void Awake()
        {
            print("Awake! segment destoyed");
        }

        private void OnTriggerEnter(Collider other)
        {
            Segment segment = other.GetComponentInParent<Segment>();
            print("Trigger Segment!!");
            if (segment != null)
            {
                Destroy(segment.gameObject);
            }
        }
    }
}
