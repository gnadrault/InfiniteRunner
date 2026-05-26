using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using World.Segment.Spawn;

namespace World.Segment
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;
        [SerializeField] private PhaseState phaseState;

        public List<SpawnPoint> SpawnPoints => spawnPoints;
        public PhaseState PhaseState => phaseState;
        
        public static event Action<Segment> OnChunkDestroyed;

        private void OnValidate()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        }

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