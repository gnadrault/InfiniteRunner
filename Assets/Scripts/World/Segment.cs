using System;
using UnityEngine;

namespace World
{
    public class Segment : MonoBehaviour
    {
        public static event Action<Segment> OnChunkDestroyed;
        public void Scroll(float speedFrame)
        {
            transform.Translate(Vector3.back * speedFrame, Space.World);
        }

        private void OnDestroy()
        {
            OnChunkDestroyed?.Invoke(this);
        }
    }
}
