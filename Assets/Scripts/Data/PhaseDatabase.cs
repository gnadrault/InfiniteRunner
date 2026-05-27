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
        [SerializeField] private string name;
        [SerializeField] private bool rainbowColor;
        [SerializeField] private Color phaseColor;
        [SerializeField] private float intensityColor;
        [SerializeField] private CameraSettings cameraSettings;
        [SerializeField] private float speed;
        [SerializeField] private float speedParticles;
        [SerializeField] private float distance;
        [SerializeField] private AudioClip music;
        
        [SerializeField] private List<Segment> newSegments;
        [SerializeField] private List<Virus> virus;

        public string Name => name;
        public bool RainbowColor => rainbowColor;
        public Color PhaseColor => phaseColor;
        public float IntensityColor => intensityColor;
        public CameraSettings CameraSettings => cameraSettings;
        public float Speed => speed;
        public float SpeedParticles => speedParticles;
        public float Distance => distance;
        public AudioClip Music => music;
        public List<Segment> NewSegments => newSegments;
        public List<Virus> Virus => virus;
        public bool IsInfiniteDistance => distance <= 0f;
    }

    [Serializable]
    public class CameraSettings
    {
        [SerializeField] private Vector3 position = new Vector3(0f, 5f, -15f);
        [SerializeField] private Vector3 rotation = new Vector3(9f, 0f, 0f);
        [SerializeField] private float fov = 25f;

        public Vector3 Position => position;
        public Vector3 Rotation => rotation;
        public float FOV => fov;
    }
}