using System.Collections.Generic;
using System.Linq;
using Core;
using Database;
using UnityEngine;
using Utils;

namespace Gameplay.Segments
{
    /// <summary>
    /// Manage segments, spawn, scroll
    /// </summary>
    public class SegmentManager : GameBehavior
    {
        public static SegmentManager Instance;
        
        [Header("Settings")] 
        [SerializeField] private int numSegments = 7;

        [Header("Segments")] 
        [SerializeField] private Segment firstSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private readonly List<Segment> _activeSegmentList = new(); // Current active segments
        private readonly List<Segment> _poolSegmentPrefabList = new(); // Pool of segments
        private Segment _lastSegmentPrefab; // Last segment, prevent to use it twice directly
        private SegmentBuilder _segmentBuilder;
        private PhaseData _currentPhaseData;
        private float _speed;
        
        public IReadOnlyList<Segment> ActiveSegments => _activeSegmentList;
        public float Speed => _speed;
        public PhaseData CurrentPhaseData => _currentPhaseData;

        /// <summary>
        /// Singleton instance
        /// Initialize the Segment Builder
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _segmentBuilder = GetComponent<SegmentBuilder>();
            }
            else
                Destroy(this);
        }

        /// <summary>
        /// Add the first empty segment to the active segments list
        /// Store segments positions for next segments
        /// </summary>
        private void Start()
        {
            _activeSegmentList.Add(firstSegment);
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
        }

        /// <summary>
        /// Subscribe to segments destroyed, player death, new phase events
        /// </summary>
        private void OnEnable()
        {
            GameEvents.OnSegmentDestroyed += RemoveSegment;
            GameEvents.OnPlayerDied += StopScroll;
            GameEvents.OnNewPhase += HandleNewPhase;
        }

        private void OnDisable()
        {
            GameEvents.OnSegmentDestroyed -= RemoveSegment;
            GameEvents.OnPlayerDied -= StopScroll;
            GameEvents.OnNewPhase -= HandleNewPhase;
        }

        /// <summary>
        /// Add a new segment to the active segments list
        /// 1. Get prefab (not same as last segment)
        /// 2. Segment builder build the new segment
        /// 3. Add it to the active segments list
        /// </summary>
        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPosition =
                new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment pooledSegmentPrefab = GetNextSegmentPrefab();
            Segment newSegment = Instantiate(pooledSegmentPrefab, spawnPosition, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, _currentPhaseData);
            _activeSegmentList.Add(newSegment);
        }

        /// <summary>
        /// Pool the next segment prefab
        /// </summary>
        /// <returns></returns>
        private Segment GetNextSegmentPrefab()
        {
            List<Segment> segmentsPool = _poolSegmentPrefabList.Where(s => s != _lastSegmentPrefab).ToList();
            Segment segment = segmentsPool[Random.Range(0, segmentsPool.Count)];
            _lastSegmentPrefab = segment;
            return segment;
        }

        /// <summary>
        /// Remove the destroyed segment to the active segments list
        /// </summary>
        /// <param name="segment"></param>
        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        /// <summary>
        /// Stop to scroll segments (player death)
        /// </summary>
        private void StopScroll()
        {
            _speed = 0f;
        }

        /// <summary>
        /// Handle when a new phase is triggered
        /// Increase speed
        /// Add new pool segments
        /// </summary>
        /// <param name="phaseData"></param>
        private void HandleNewPhase(PhaseData phaseData)
        {
            _currentPhaseData = phaseData;
            _speed = phaseData.Speed;
            GameEvents.OnSpeedChanged?.Invoke(_speed);
            _poolSegmentPrefabList.AddRange(phaseData.NewSegments);
        }

        protected override void GameplayUpdate()
        {
            while (_activeSegmentList.Count < numSegments) // Always keep minimum number of segments (1 destoyed = 1 created)
                AddSegment();

            foreach (Segment segment in _activeSegmentList) // Scroll all segments with the same speed
                segment.Scroll(_speed * Time.deltaTime);
        }
    }
}