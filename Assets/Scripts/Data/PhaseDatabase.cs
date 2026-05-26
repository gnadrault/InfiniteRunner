using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PhaseDatabase", menuName = "SyntaxError/PhaseDatabase")]
    public class PhaseDatabase : ScriptableObject
    {
        [SerializeField] public List<PhaseData> phases;
        
        // TODO Check if last phase => infinite
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
    }
}