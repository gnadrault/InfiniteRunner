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

        public Segment GetPrefab(PhaseState currentPhase, Segment exceptSegment)
        {
            return GetPrefab(currentPhase, new List<Segment> { exceptSegment });
        }

        public Segment GetPrefab(PhaseState currentPhase, List<Segment> exceptSegments)
        {
            List<Segment> segmentsPoolPhase =
                segments.Where(s => !exceptSegments.Contains(s) && s.PhaseState <= currentPhase).ToList();
            return segmentsPoolPhase[Random.Range(0, segmentsPoolPhase.Count)];
        }
    }
}