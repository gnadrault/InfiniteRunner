using System.Collections.Generic;
using System.Linq;
using Gameplay.Segment;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SegmentDatabase", menuName = "SyntaxError/SegmentDatabase")]
    public class SegmentDatabase : ScriptableObject
    {
        [SerializeField] public List<Segment> segments;
    }
}