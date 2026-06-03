using Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Feedback
{
    /// <summary>
    /// Game effect to obscure the screen
    /// </summary>
    public class ObscureScreenFeedback : Feedback
    {
        [Header("References")]
        [SerializeField] private Volume globalVolume;
        
        [Header("Settings")]
        [SerializeField] private float transitionDuration = 0.4f;

        private Bloom _bloom;

        private void Awake()
        {
            globalVolume.profile.TryGet(out _bloom);
        }

        protected override void ApplyEffect(FeedbackProfile profile)
        {
            if (!profile || !profile.ObscureScreen.enabled) return;
            ObscureScreenSection data = profile.ObscureScreen;
            
            StopCurrent();
            _bloom.dirtTexture.value = data.dirtTexture;
            routine = StartCoroutine(TweenUtils.Transition(t =>
                    _bloom.dirtIntensity.value = Mathf.Lerp(0f, data.dirtIntensity, t),
                transitionDuration
            ));
        }

        protected override void ResetToAmbient()
        {
            StopCurrent();
            routine = StartCoroutine(TweenUtils.Transition(t =>
                    _bloom.dirtIntensity.value = Mathf.Lerp(_bloom.dirtIntensity.value, 0.001f, t),
                transitionDuration
            ));
        }
    }
}