using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using Gameplay.Segments;
using UnityEngine;
using Utils;

namespace Gameplay
{
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

        private readonly List<Segment> _activeSegmentList = new();
        private readonly List<Segment> _poolSegmentPrefabList = new();
        private Segment _lastSegmentPrefab;
        private SegmentBuilder _segmentBuilder;
        private PhaseData _currentPhaseData;
        private float _speed;
        
        public IReadOnlyList<Segment> ActiveSegments => _activeSegmentList;
        public float Speed => _speed;

        
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

        private void Start()
        {
            _activeSegmentList.Add(firstSegment);
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
        }

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

        private Segment GetNextSegmentPrefab()
        {
            List<Segment> segmentsPool = _poolSegmentPrefabList.Where(s => s != _lastSegmentPrefab).ToList();
            Segment segment = segmentsPool[Random.Range(0, segmentsPool.Count)];
            _lastSegmentPrefab = segment;
            return segment;
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void StopScroll()
        {
            _speed = 0f;
        }

        private void HandleNewPhase(PhaseData phaseData)
        {
            _currentPhaseData = phaseData;
            _speed = phaseData.Speed;
            GameEvents.OnSpeedChanged?.Invoke(_speed);
            _poolSegmentPrefabList.AddRange(phaseData.NewSegments);
        }

        protected override void GameplayUpdate()
        {
            while (_activeSegmentList.Count < numSegments)
                AddSegment();

            foreach (Segment segment in _activeSegmentList)
                segment.Scroll(_speed * Time.deltaTime);
        }
    }
}