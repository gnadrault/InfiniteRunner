using System.Collections.Generic;
using Data;
using UnityEngine;
using Utils;

namespace World.Segment
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private int maxSegments = 5;

        [Header("Segments")] [SerializeField] private Segment firstSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;
        [SerializeField] private SegmentDatabase segmentDatabase;
        [SerializeField] private PhaseDatabase phasesDatabase;
        
        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private List<Segment> _activeSegmentList;
        private List<Segment> _poolSegmentList;
        private int _currentPhaseIndex;
        private SegmentBuilder _segmentBuilder;

        public float ScrollSpeed => phasesDatabase.phases[_currentPhaseIndex].speed;
        public PhaseState CurrentPhaseState => phasesDatabase.phases[_currentPhaseIndex].phaseState;

        private void Awake()
        {
            _segmentBuilder = GetComponent<SegmentBuilder>();
        }

        private void Start()
        {
            _activeSegmentList = new List<Segment> { firstSegment };
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
        }

        private void OnEnable()
        {
            Segment.OnChunkDestroyed += RemoveSegment;
            GameEvents.OnPlayerDied += StopScroll;
            GameEvents.OnNewMeter += CheckPhase;
        }

        private void OnDisable()
        {
            Segment.OnChunkDestroyed -= RemoveSegment;
            GameEvents.OnPlayerDied -= StopScroll;
            GameEvents.OnNewMeter -= CheckPhase;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPos = new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment pooledSegment =
                segmentDatabase.GetPrefab(phasesDatabase.phases[_currentPhaseIndex].phaseState);
            Segment newSegment = Instantiate(pooledSegment, spawnPos, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, phasesDatabase.phases[_currentPhaseIndex].phaseState, ScrollSpeed); // TODO: Pass the current phase info (speed, color, spawn rate, ...)
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void StopScroll()
        {
            phasesDatabase.phases[_currentPhaseIndex].speed = 0f;
        }

        private void CheckPhase(float distance)
        {
            if (distance >= phasesDatabase.phases[_currentPhaseIndex].distance 
                && _currentPhaseIndex < phasesDatabase.phases.Count - 1)
                _currentPhaseIndex++;
        }

        private void Update()
        {
            while (_activeSegmentList.Count < maxSegments)
            {
                AddSegment();
            }

            foreach (Segment segment in _activeSegmentList)
            {
                segment.Scroll(phasesDatabase.phases[_currentPhaseIndex].speed * Time.deltaTime);
            }
        }
    }
}