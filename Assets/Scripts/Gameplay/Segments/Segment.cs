using System.Collections.Generic;
using System.Linq;
using Core;
using Gameplay.Elements.Obstacles;
using Gameplay.Segments.Spawn;
using UnityEngine;
using Utils;

namespace Gameplay.Segments
{
    /// <summary>
    /// Segment game object 
    /// </summary>
    public class Segment : GameBehavior
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;
        [SerializeField] private GameObject segmentMesh;

        public List<SpawnPoint> SpawnPoints => spawnPoints;
        private List<Renderer> _renderers;
        
        private void Awake()
        {
            //  Get all renderers (segment + obstacles) from the current segment game object
            _renderers = new List<Renderer>(segmentMesh.GetComponentsInChildren<Renderer>());
            Obstacle[] obstacles = GetComponentsInChildren<Obstacle>();
            foreach (Obstacle obstacle in obstacles)
            {
                Renderer[] renderers = obstacle.GetComponentsInChildren<Renderer>();
                _renderers.AddRange(renderers);
            }
        }

        private void OnValidate()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        }

        /// <summary>
        /// Toggle renderers, used for the stroboscope effect
        /// </summary>
        /// <param name="active"></param>
        public void ToggleBlink(bool active)
        {
            foreach (Renderer r in _renderers)
                r.enabled = active;
        }

        /// <summary>
        /// Scrolling segment
        /// </summary>
        /// <param name="speedFrame"></param>
        public void Scroll(float speedFrame)
        {
            transform.Translate(Vector3.back * speedFrame, Space.World);
        }

        private void OnDestroy() => GameEvents.OnSegmentDestroyed?.Invoke(this);
    }
}