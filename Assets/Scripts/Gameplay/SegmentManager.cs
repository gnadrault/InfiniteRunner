using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int maxSegments = 5;

        [Header("Segments")] 
        [SerializeField] private Segment.Segment firstSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private readonly List<Segment.Segment> _activeSegmentList = new();
        private readonly List<Segment.Segment> _poolSegmentList = new();
        private SegmentBuilder _segmentBuilder;
        private PhaseData _currentPhaseData;

        public float ScrollSpeed => _currentPhaseData.speed;
        public PhaseState CurrentPhaseState => _currentPhaseData.phaseState;

        private void Awake()
        {
            _segmentBuilder = GetComponent<SegmentBuilder>();
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
            Segment.Segment.OnChunkDestroyed += RemoveSegment;
            GameEvents.OnPlayerDied += StopScroll;
            GameEvents.OnNewPhase += HandleNewPhase;
        }

        private void OnDisable()
        {
            Segment.Segment.OnChunkDestroyed -= RemoveSegment;
            GameEvents.OnPlayerDied -= StopScroll;
            GameEvents.OnNewPhase -= HandleNewPhase;
        }

        private void AddSegment()
        {
            Segment.Segment lastSegment = _activeSegmentList[^1]; // Prevent to pull same segment twice
            Vector3 spawnPosition =
                new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment.Segment pooledSegment = GetNextSegment(lastSegment);
            Segment.Segment newSegment = Instantiate(pooledSegment, spawnPosition, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, _currentPhaseData);
            _activeSegmentList.Add(newSegment);
        }

        private Segment.Segment GetNextSegment(Segment.Segment lastSegment)
        {
            List<Segment.Segment> segmentsPool = _poolSegmentList.Where(s => s != lastSegment).ToList();
            return segmentsPool[Random.Range(0, segmentsPool.Count)];
        }

        private void RemoveSegment(Segment.Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void StopScroll()
        {
            _currentPhaseData.speed = 0f;
        }

        private void HandleNewPhase(PhaseData phaseData)
        {
            _currentPhaseData = phaseData;
            _poolSegmentList.AddRange(phaseData.newSegments.segments);
        }

        private void Update()
        {
            while (_activeSegmentList.Count < maxSegments)
            {
                AddSegment();
            }

            foreach (Segment.Segment segment in _activeSegmentList)
            {
                segment.Scroll(_currentPhaseData.speed * Time.deltaTime);
            }
        }
    }
}