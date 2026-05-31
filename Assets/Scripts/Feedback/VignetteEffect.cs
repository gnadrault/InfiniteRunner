using System.Collections;
using Core;
using Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Feedback
{
    public class VignetteEffect : GameBehavior, IGameFeelEffect
    {
        [SerializeField] private float hitDuration = 1f;
        [SerializeField] private Volume globalVolume;
        [SerializeField] private float transitionDuration = 0.4f;
        
        private Vignette _vignette;
        private Coroutine _routine;

        private void OnEnable()
        {
            GameEvents.OnGameFeelProfileStart += ApplyEffect;
            GameEvents.OnGameFeelEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnGameFeelProfileStart -= ApplyEffect;
            GameEvents.OnGameFeelEnd -= ResetToAmbient;
        }

        private void Awake()
        {
            globalVolume.profile.TryGet(out _vignette);
        }

        public void ApplyEffect(GameFeelProfile profile)
        {
            if (!profile || !profile.Vignette.enabled) return;
            VignetteSection data = profile.Vignette;

            StopCurrent();
            _vignette.smoothness.value = data.smoothness;
            _routine = data.pulse
                ? StartCoroutine(PulseRoutine(data))
                : StartCoroutine(FadeTo(data.color, data.intensity));
        }

        private IEnumerator FadeTo(Color targetColor, float targetIntensity)
        {
            _vignette.active = true;
            _vignette.color.value = targetColor;

            float startIntensity = _vignette.intensity.value;

            yield return TweenUtils.Transition(t =>
                    _vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, t),
                transitionDuration
            );

            if (Mathf.Approximately(targetIntensity, 0f))
                _vignette.active = false;

            _routine = null;
        }

        private IEnumerator PulseRoutine(VignetteSection data)
        {
            _vignette.active = true;
            _vignette.color.value = data.color;
            
            while (true)
            {
                float t = Mathf.PingPong(Time.time * data.pulseSpeed, 1f);
                _vignette.intensity.value = Mathf.Lerp(0f, data.intensity, t);
                yield return null;
            }
        }
        
        public void ResetToAmbient()
        {
            StopCurrent();
            _routine = StartCoroutine(FadeTo(_vignette.color.value, 0f));
        }

        private void StopCurrent()
        {
            if (_routine == null) return;
            StopCoroutine(_routine);
            _routine = null;
        }
    }
}