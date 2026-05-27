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
        public CameraSettings cameraSettings;
        public float speed;
        public float distance;
        public AudioClip music;
        
        public List<Segment> newSegments;
        public List<Virus> virus;
        
        public bool IsInfiniteDistance => distance <= 0f;
    }

    [Serializable]
    public class CameraSettings
    {
        public Vector3 position = new Vector3(0f, 5f, -15f);
        public Vector3 rotation = new Vector3(9f, 0f, 0f);
        public float fov = 25f;
    }
}