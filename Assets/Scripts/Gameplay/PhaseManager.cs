using Data;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class PhaseManager : MonoBehaviour
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        
        [SerializeField] private PhaseDatabase phasesDatabase;
        [SerializeField] private Material tronMaterial;
        
        private int _currentPhaseIndex;

        private void Start()
        {
            ChangePhase(0);
        }

        private void OnEnable()
        {
            GameEvents.OnNewMeter += CheckPhase;
        }

        private void OnDisable()
        {
            GameEvents.OnNewMeter -= CheckPhase;
        }

        private void CheckPhase(float distance)
        {
            PhaseData currentPhase = phasesDatabase.phases[_currentPhaseIndex];
            if (distance < currentPhase.distance || currentPhase.IsInfiniteDistance)
                return;
            
            ChangePhase(_currentPhaseIndex + 1);
            GameEvents.OnNewPhase?.Invoke(phasesDatabase.phases[_currentPhaseIndex]);
        }

        private void ChangePhase(int newPhaseIndex)
        {
            _currentPhaseIndex = newPhaseIndex;
            tronMaterial.SetColor(EmissionColor,
                phasesDatabase.phases[_currentPhaseIndex].phaseColor *
                phasesDatabase.phases[_currentPhaseIndex].intensityColor);
        }
    }
}