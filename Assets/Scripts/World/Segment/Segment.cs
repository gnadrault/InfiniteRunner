using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using World.Spawn;

namespace World.Segment
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;
        [SerializeField] private PhaseState phaseState;

        public List<SpawnPoint> SpawnPoints => spawnPoints;
        public PhaseState PhaseState => phaseState;

        private void OnValidate()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        }

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