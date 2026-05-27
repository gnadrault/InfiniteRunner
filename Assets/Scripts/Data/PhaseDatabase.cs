using System;
using System.Collections.Generic;
using Gameplay.Elements.Ennemis;
using Gameplay.Segments;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PhaseDatabase", menuName = "SyntaxError/PhaseDatabase")]
    public class PhaseDatabase : ScriptableObject
    {
        public List<PhaseData> phases;
    }
    
    [Serializable]
    public class PhaseData
    {
        public string name;
        public bool rainbowColor;
        public Color phaseColor;
        public float intensityColor;
        public float cameraAngle;
        public float speed;
        public float distance;
        public AudioClip music;
        
        public List<Segment> newSegments;
        public List<Virus> virus;
        
        public bool IsInfiniteDistance => distance <= 0f;
    }
}