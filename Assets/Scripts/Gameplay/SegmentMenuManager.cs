using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Segments;
using UnityEngine;

namespace Gameplay
{
    public class SegmentMenuManager : MonoBehaviour
    {
         [Header("Settings")] 
        [SerializeField] private int numSegments = 7;

        [Header("Segments")] 
        [SerializeField] private Segment firstSegment;
        [SerializeField] private Segment defaultSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;
        [SerializeField] private float speed = 30f;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private readonly List<Segment> _activeSegmentList = new();
        private SegmentBuilder _segmentBuilder;

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
            Segment.OnSegmentDestroyed += RemoveSegment;
        }

        private void OnDisable()
        {
            Segment.OnSegmentDestroyed -= RemoveSegment;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPosition =
                new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment newSegment = Instantiate(defaultSegment, spawnPosition, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, null);
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void Update()
        {
            while (_activeSegmentList.Count < numSegments)
                AddSegment();

            foreach (Segment segment in _activeSegmentList)
                segment.Scroll(speed * Time.deltaTime);
        }
    }
}