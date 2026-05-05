using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace World.Segment
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int maxSegments = 5;
        [SerializeField] private float scrollSpeed = 2f;

        [Header("Segments")] 
        [SerializeField] private Segment firstSegment;

        [SerializeField] private int segmentsCountPerPhase = 5;
        [SerializeField] private SegmentDatabase segmentDatabase;
        [SerializeField] private SegmentBuilder segmentGenerator;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private List<Segment> _activeSegmentList;
        private List<Segment> _poolSegmentList;
        private PhaseState _currentPhaseState;

        private void Start()
        {
            _activeSegmentList = new List<Segment> { firstSegment };
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
            _currentPhaseState = PhaseState.Phase1;
        }

        private void OnEnable()
        {
            Segment.OnChunkDestroyed += RemoveSegment;
        }

        private void OnDisable()
        {
            Segment.OnChunkDestroyed -= RemoveSegment;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPos = new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);
            
            Segment pooledSegment =
                segmentDatabase.GetPrefab(_currentPhaseState,
                    lastSegment); // TODO change on current phase, never same twice
            Segment newSegment = Instantiate(pooledSegment, spawnPos, Quaternion.identity);

            segmentGenerator.GenerateSegmentObjects(newSegment,
                _currentPhaseState); // TODO pass the right pool segments instead of index (the pool should be here, in the manager???)
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
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