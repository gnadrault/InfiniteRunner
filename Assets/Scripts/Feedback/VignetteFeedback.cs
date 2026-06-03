using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Feedback
{
    /// <summary>
    /// Game effect add pulse or fixed vignette around the screen
    /// </summary>
    public class VignetteFeedback : Feedback
    {
        [Header("References")]
        [SerializeField] private Volume globalVolume;
        
        [Header("Settings")]
        [SerializeField] private float hitDuration = 1f;
        [SerializeField] private float transitionDuration = 0.4f;
        
        private Vignette _vignette;

        private void OnEnable()
        {
            GameEvents.OnFeedbackStart += ApplyEffect;
            GameEvents.OnFeedbackEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnFeedbackStart -= ApplyEffect;
            GameEvents.OnFeedbackEnd -= ResetToAmbient;
        }

        private void Awake()
        {
            globalVolume.profile.TryGet(out _vignette);
        }

        protected override void ApplyEffect(FeedbackProfile profile)
        {
            if (!profile || !profile.Vignette.enabled) return;
            VignetteSection data = profile.Vignette;

            StopCurrent();
            _vignette.smoothness.value = data.smoothness;
            routine = data.pulse
                ? StartCoroutine(PulseRoutine(data))
                : StartCoroutine(FadeTo(data.color, data.intensity));
        }
        
        protected override void ResetToAmbient()
        {
            StopCurrent();
            routine = StartCoroutine(FadeTo(_vignette.color.value, 0.001f));
        }

        private IEnumerator FadeTo(Color targetColor, float targetIntensity)
        {
            _vignette.color.value = targetColor;

            float startIntensity = _vignette.intensity.value;

            yield return TweenUtils.Transition(t =>
                    _vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, t),
                transitionDuration
            );

            routine = null;
        }

        private IEnumerator PulseRoutine(VignetteSection data)
        {
            _vignette.color.value = data.color;
            
            while (true)
            {
                float t = Mathf.PingPong(Time.time * data.pulseSpeed, 1f);
                _vignette.intensity.value = Mathf.Lerp(0.001f, data.intensity, t);
                yield return null;
            }
        }
    }
}