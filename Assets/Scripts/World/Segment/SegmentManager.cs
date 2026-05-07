using System.Collections.Generic;
using Data;
using Player;
using UnityEngine;

namespace World.Segment
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private int maxSegments = 5;
        [SerializeField] private float scrollSpeed = 2f;

        [Header("Segments")] [SerializeField] private Segment firstSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;
        [SerializeField] private SegmentDatabase segmentDatabase;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private List<Segment> _activeSegmentList;
        private List<Segment> _poolSegmentList;
        private PhaseState _currentPhaseState;
        private SegmentBuilder _segmentBuilder;

        public float ScrollSpeed => scrollSpeed;

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
            _currentPhaseState = PhaseState.Phase4;
        }

        private void OnEnable()
        {
            Segment.OnChunkDestroyed += RemoveSegment;
            PlayerController.OnPlayerDied += StopScroll;
        }

        private void OnDisable()
        {
            Segment.OnChunkDestroyed -= RemoveSegment;
            PlayerController.OnPlayerDied -= StopScroll;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPos = new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment pooledSegment =
                segmentDatabase.GetPrefab(_currentPhaseState);
            Segment newSegment = Instantiate(pooledSegment, spawnPos, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, _currentPhaseState);
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void StopScroll()
        {
            scrollSpeed = 0f;
        }

        private void Update()
        {
            while (_activeSegmentList.Count < maxSegments)
            {
                AddSegment();
            }

            foreach (Segment segment in _activeSegmentList)
            {
                segment.Scroll(scrollSpeed * Time.deltaTime);
            }
        }
    }
}