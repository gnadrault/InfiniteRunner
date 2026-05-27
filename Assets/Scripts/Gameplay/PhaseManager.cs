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
        [SerializeField] private float speedTransition = 2f;

        private int _currentPhaseIndex;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            ChangePhase(0);
            TimeScaleManager.Instance.SetTimeScale(1f);
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
            Color initialColor = tronMaterial.GetColor(EmissionColor);
            Color targetColor = phasesDatabase.phases[_currentPhaseIndex].phaseColor;
            float intensity = phasesDatabase.phases[_currentPhaseIndex].intensityColor;
            StartCoroutine(TweenUtils.Transition(t =>
                    tronMaterial.SetColor(EmissionColor, Color.Lerp(initialColor, targetColor * intensity, t)),
                speedTransition
            ));
        }

        private void UpdateCamera()
        {
            float initialFOV = _mainCamera.fieldOfView;
            float targetFOV = phasesDatabase.phases[_currentPhaseIndex].cameraSettings.fov;
            StartCoroutine(TweenUtils.Transition(t =>
                    _mainCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t),
                speedTransition
            ));
        }
    }
}