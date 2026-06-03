using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

namespace Gameplay.Segments
{
    /// <summary>
    /// Manage the segments spawner, scroller only for the main menu
    /// </summary>
    public class SegmentMenuManager : GameBehavior
    {
        [Header("Settings")] 
        [SerializeField] private int numSegments = 7;

        [Header("Segments")] 
        [SerializeField] private Segment firstSegment;
        [SerializeField] private Segment fakeSegmentPrefab;
        [SerializeField] private float speed = 30f;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private readonly List<Segment> _activeSegmentList = new();

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
        }

        private void OnDisable()
        {
            GameEvents.OnSegmentDestroyed -= RemoveSegment;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1];
            Vector3 spawnPosition = new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);
            
            Segment newSegment = Instantiate(fakeSegmentPrefab, spawnPosition, Quaternion.identity);
            
            _activeSegmentList.Add(newSegment);
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        protected override void GameplayUpdate()
        {
            while (_activeSegmentList.Count < numSegments)
                AddSegment();

            foreach (Segment segment in _activeSegmentList)
                segment.Scroll(speed * Time.deltaTime);
        }
    }
}