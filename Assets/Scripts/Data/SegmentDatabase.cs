using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.Segment;

namespace Data
{
    [CreateAssetMenu(fileName = "SegmentDatabase", menuName = "SyntaxError/SegmentDatabase")]
    public class SegmentDatabase : ScriptableObject
    {
        [SerializeField] private List<Segment> segments;

        private Segment _lastSegment; // Prevent to pull same segment twice

        public Segment GetPrefab(PhaseState currentPhase)
        {
            List<Segment> segmentsPoolPhase =
                segments.Where(s => s != _lastSegment && s.PhaseState <= currentPhase).ToList();
            Segment segmentPooled = segmentsPoolPhase[Random.Range(0, segmentsPoolPhase.Count)];
            _lastSegment = segmentPooled;
            return segmentPooled;
        }
    }
}