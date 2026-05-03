using System.Collections.Generic;
using UnityEngine;

namespace World.Segment
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxSegments = 5;
        [SerializeField] private float scrollSpeed = 2f;
        
        [Header("Segments")]
        [SerializeField] private Data.Segment firstSegment;
        
        [SerializeField] private int segmentsCountPerPhase = 5;
        [SerializeField] private List<Data.Segment> segmentsPoolPhase1;
        [SerializeField] private SegmentGeneration segmentGenerator;
        
        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;
        
        private List<Data.Segment> _activeSegmentList;
        private List<Data.Segment> _poolSegmentList;

        private void Start()
        {
            _activeSegmentList = new List<Data.Segment> { firstSegment };
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
        }

        private void OnEnable()
        {
            Data.Segment.OnChunkDestroyed += RemoveSegment;
        }

        private void OnDisable()
        {
           Data.Segment.OnChunkDestroyed -= RemoveSegment;
        }

        private void AddSegment()
        {
            Data.Segment lastSegment = _activeSegmentList[_activeSegmentList.Count - 1];
            Vector3 spawnPos = new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);
            Data.Segment pooledSegment = segmentsPoolPhase1[Random.Range(0, segmentsPoolPhase1.Count)]; // TODO change on current phase, never same twice
            Data.Segment newSegment = Instantiate(pooledSegment, spawnPos, Quaternion.identity);
            segmentGenerator.GenerateSegmentObjects(newSegment, 0); // TODO pass the right pool segments instead of index (the pool should be here, in the manager???)
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Data.Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void Update()
        {
            while (_activeSegmentList.Count < maxSegments)
            {
                AddSegment();
            }
            foreach (Data.Segment segment in _activeSegmentList)
            {
                segment.Scroll(scrollSpeed * Time.deltaTime);
            }
        }
    }
}
