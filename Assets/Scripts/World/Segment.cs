using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;

        public List<SpawnPoint> SpawnPoints => spawnPoints;

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
