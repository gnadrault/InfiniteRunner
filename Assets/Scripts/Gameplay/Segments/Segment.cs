using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Segments.Spawn;
using UnityEngine;

namespace Gameplay.Segments
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;

        public List<SpawnPoint> SpawnPoints => spawnPoints;
        
        public static event Action<Segment> OnSegmentDestroyed;

        private void OnValidate()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        }

        public void Scroll(float speedFrame)
        {
            transform.Translate(Vector3.back * speedFrame, Space.World);
        }

        private void OnDestroy() => OnSegmentDestroyed?.Invoke(this);
    }
}