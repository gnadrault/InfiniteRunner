using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PhaseDatabase", menuName = "SyntaxError/PhaseDatabase")]
    public class PhaseDatabase : ScriptableObject
    {
        [SerializeField] public List<PhaseData> phases;
    }
    
    [Serializable]
    public class PhaseData
    {
        [SerializeField] public PhaseState phaseState;
        [SerializeField] public bool rainbowColor;
        [SerializeField] public Color phaseColor;
        [SerializeField] public float intensityColor;
        [SerializeField] public float cameraAngle;
        [SerializeField] public float speed;
        [SerializeField] public float distance;
        [SerializeField] public AudioClip music;

        [SerializeField] public SegmentDatabase newSegments;
        [SerializeField] public VirusDatabase virus;
        
        public bool IsInfiniteDistance => distance <= 0f;
    }
}