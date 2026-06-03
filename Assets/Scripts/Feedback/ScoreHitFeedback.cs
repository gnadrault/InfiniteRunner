using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Feedback
{
    /// <summary>
    /// Game effect shake and hit the text label
    /// </summary>
    public class ScoreHitFeedback : Feedback
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI label;
        
        [Header("Settings")]
        [SerializeField] private float hitDuration = 0.4f;

        private Vector3 _initialPos;
        private Color _initialColor;

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
            _initialPos = label.transform.localPosition;
            _initialColor = label.color;
        }

        protected override void ApplyEffect(FeedbackProfile profile)
        {
            if (!profile || !profile.Score.enabled) return;

            StopCurrent();
            routine = StartCoroutine(PulseRoutine(profile.Score));
        }

        protected override void ResetToAmbient()
        {
            StopCurrent();
            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
        }

        private IEnumerator PulseRoutine(ScoreSection scoreProfil)
        {
            while (true)
            {
                yield return SingleHit(scoreProfil);
                yield return new WaitForSeconds(1f - hitDuration);
            }
        }

        private IEnumerator SingleHit(ScoreSection scoreProfil)
        {
            float elapsed = 0f;

            _initialPos = label.transform.localPosition;
            _initialColor = label.color;

            while (elapsed < hitDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / hitDuration;

                label.transform.localPosition = _initialPos + Random.insideUnitSphere * scoreProfil.shakeIntensity;
                label.color = Color.Lerp(scoreProfil.color, _initialColor, t);
                yield return null;
            }

            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
        }
    }
}