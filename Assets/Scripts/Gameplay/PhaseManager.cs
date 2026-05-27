using System;
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
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

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
        }

        private void ChangePhase(int newPhaseIndex)
        {
            _currentPhaseIndex = newPhaseIndex;
            GameEvents.OnNewPhase?.Invoke(phasesDatabase.phases[_currentPhaseIndex]);
            
            UpdateColors();
            UpdateCamera();
        }

        private void UpdateColors()
        {
            tronMaterial.SetColor(EmissionColor,
                phasesDatabase.phases[_currentPhaseIndex].phaseColor *
                phasesDatabase.phases[_currentPhaseIndex].intensityColor);
        }
        
        private void UpdateCamera()
        {
            _mainCamera.transform.position = phasesDatabase.phases[_currentPhaseIndex].cameraSettings.position;
            _mainCamera.transform.rotation = Quaternion.Euler(phasesDatabase.phases[_currentPhaseIndex].cameraSettings.rotation);
            _mainCamera.fieldOfView = phasesDatabase.phases[_currentPhaseIndex].cameraSettings.fov;
        }
    }
}