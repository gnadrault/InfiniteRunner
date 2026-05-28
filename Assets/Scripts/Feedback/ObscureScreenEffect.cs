using Feedback.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Feedback
{
    public class ObscureScreenEffect : MonoBehaviour, IGameFeelEffect
    {
        [SerializeField] private Volume globalVolume;
        [SerializeField] private float transitionDuration = 0.4f;

        private Bloom _bloom;
        private Coroutine _routine;

        private void OnEnable()
        {
            GameEvents.OnGameFeelProfile += ApplyEffect;
            GameEvents.OnGameFeelEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnGameFeelProfile -= ApplyEffect;
            GameEvents.OnGameFeelEnd += ResetToAmbient;
        }

        private void Awake()
        {
            globalVolume.profile.TryGet(out _bloom);
        }

        public void ApplyEffect(GameFeelProfile profile)
        {
            if (!profile || !profile.ObscureScreen.enabled) return;
            ObscureScreenSection data = profile.ObscureScreen;
            
            _bloom.dirtTexture.value = data.dirtTexture;
            StartCoroutine(TweenUtils.Transition(t =>
                    _bloom.dirtIntensity.value = Mathf.Lerp(0f, data.dirtIntensity, t),
                transitionDuration
            ));
        }

        public void ResetToAmbient()
        {
            StartCoroutine(TweenUtils.Transition(t =>
                    _bloom.dirtIntensity.value = Mathf.Lerp(_bloom.dirtIntensity.value, 0f, t),
                transitionDuration
            ));
        }
    }
}