using System;
using UnityEngine;

namespace World
{
    public class Chunk : MonoBehaviour
    {
        public static event Action<Chunk> OnChunkDestroyed;
        public void Scroll(float speedFrame)
        {
            transform.Translate(Vector3.back * speedFrame);
        }

        private void OnDestroy()
        {
            OnChunkDestroyed?.Invoke(this);
        }
    }
}
