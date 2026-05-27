using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Feedback
{
    public class VignetteEffect : MonoBehaviour
    {
        [SerializeField] private float hitDuration = 1f;
        [SerializeField] private Volume globalVolume;
        private Vignette _vignette;

        private void Awake()
        {
            globalVolume.profile.TryGet(out _vignette);
        }

        private void OnEnable()
        {
            GameEvents.OnRemovePercentPoints += OnHit; // TODO: When virus attached
        }

        private void OnDisable()
        {
            GameEvents.OnRemovePercentPoints -= OnHit;
        }
        
        private void OnHit(float obj)
        {
            StartCoroutine(VignetteHit());
        }

        private IEnumerator VignetteHit()
        {
            float elapsed = 0f;
            while (elapsed < hitDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / hitDuration;
                _vignette.intensity.value = Mathf.Lerp(0.38f, 0f, t);
                yield return null;
            }
            _vignette.intensity.value = 0f;
        }
    }
}