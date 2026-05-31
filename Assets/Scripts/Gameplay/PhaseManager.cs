using Core;
using Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Gameplay
{
    public class PhaseManager : GameBehavior
    {
        [SerializeField] private Volume globalVolume;
        [SerializeField] private PhaseDatabase phasesDatabase;
        [SerializeField] private ParticleSystem envParticles;
        [SerializeField] private float speedTransition = 2f;

        private int _currentPhaseIndex;
        private Camera _mainCamera;
        private Bloom _bloom;

        private void Awake()
        {
            _mainCamera = Camera.main;
            globalVolume.profile.TryGet(out _bloom);
        }

        private void Start()
        {
            ChangePhase(0);
            TimeManager.Instance.SetTimeScale(1f);
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
            if (distance < currentPhase.Distance || currentPhase.IsInfiniteDistance)
                return;

            ChangePhase(_currentPhaseIndex + 1);
        }

        private void ChangePhase(int newPhaseIndex)
        {
            _currentPhaseIndex = newPhaseIndex;
            GameEvents.OnNewPhase?.Invoke(phasesDatabase.phases[_currentPhaseIndex]);

            UpdatePhaseColor();
            UpdateParticles();
            UpdateCamera();
        }

        private void UpdatePhaseColor()
        {
            Color initialColor = _bloom.tint.value;
            Color targetColor = phasesDatabase.phases[_currentPhaseIndex].PhaseColor;
            float intensity = phasesDatabase.phases[_currentPhaseIndex].IntensityColor;
            StartCoroutine(TweenUtils.Transition(t =>
                _bloom.tint.value = Color.Lerp(initialColor, targetColor * intensity, t),
                speedTransition
            ));
        }

        private void UpdateParticles()
        {
            // Color
            Color initialStartColor = envParticles.main.startColor.color;
            Color targetColor = phasesDatabase.phases[_currentPhaseIndex].PhaseColor;
            StartCoroutine(TweenUtils.Transition(t =>
            {
                var main = envParticles.main;
                main.startColor = Color.Lerp(initialStartColor, targetColor, t);
            }, speedTransition));

            // Speed
            float initialSimulationSpeed = envParticles.main.simulationSpeed;
            float targetSimulationSpeed = phasesDatabase.phases[_currentPhaseIndex].SpeedParticles;
            StartCoroutine(TweenUtils.Transition(t =>
            {
                var main = envParticles.main;
                main.simulationSpeed = Mathf.Lerp(initialSimulationSpeed, targetSimulationSpeed, t);
            }, speedTransition));
        }

        private void UpdateCamera()
        {
            Vector3 initialCameraPosition = _mainCamera.transform.position;
            Vector3 targetCameraPosition = new Vector3(_mainCamera.transform.position.x,
                phasesDatabase.phases[_currentPhaseIndex].CameraSettings.Position.y, _mainCamera.transform.position.z);

            float initialFOV = _mainCamera.fieldOfView;
            float targetFOV = phasesDatabase.phases[_currentPhaseIndex].CameraSettings.FOV;
            StartCoroutine(TweenUtils.Transition(t =>
            {
                _mainCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t);
                _mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, t);
            }, speedTransition));
        }
    }
}